using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.Application.Contracts;
using TestTask.Application.Extensions;
using TestTask.Application.Services.Interfaces;
using TestTask.DAL.PostgreSQL;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

internal class WeatherService(
    TestTaskDbContext context,
    ILogger<WeatherService> logger,
    IValidator<WeatherArchiveAddDTO> weatherArchiveAddDtoValidator,
    IFileReader<WeatherRecordAddDTO> fileReader)
    : IWeatherService
{
    public async Task<Response> AddArchiveAsync(
        WeatherArchiveAddDTO dto, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var validationResult = await weatherArchiveAddDtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Response.Failure(validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage)));
        }

        var archive = new WeatherArchive()
        {
            Title = dto.Title,
            DateAdded = TimeProvider.System.GetUtcNow(),
            LocalityTitle = dto.LocalityTitle,
            LocalityTimeZoneId = dto.LocalityTimeZoneId
        };
        
        try
        {
            var records = fileReader
                .Enumerate(dto.FilePath)
                .Select(e => e.ToRecord(archive));

            context.WeatherArchives.Add(archive);
            context.WeatherRecords.AddRange(records);
            
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something went wrong while adding archive.");
            return Response.Failure(new Error($"{nameof(WeatherService)}.{nameof(AddArchiveAsync)}", "Something went wrong while adding archive."));
        }

        return Response.Success();
    }

    public async Task<Response<Page<WeatherArchiveDTO>>> GetArchivesPageAsync(
        PagingOptions? pagingOptions = null, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var totalArchivesCount = context.WeatherArchives.Count();
        var result = await context
            .WeatherArchives
            .AsNoTracking()   
            .Include(e => e.WeatherRecords)
            .OrderBy(e => e.DateAdded)
            .ApplyPaging(pagingOptions)
            .Select(e => e.ToDTO())
            .ToListAsync(cancellationToken);

        return new Page<WeatherArchiveDTO>(result, totalArchivesCount, pagingOptions);
    }

    public async Task<Response<WeatherRecordsPage>> GetRecordsPageAsync(
        Guid weatherArchiveId, 
        PagingOptions? pagingOptions = null,
        WeatherRecordsFilteringOptions? filteringOptions = null, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var weatherArchive = await context.WeatherArchives.FirstOrDefaultAsync(e => e.Id == weatherArchiveId, cancellationToken);
        if (weatherArchive is null)
        {
            return Response.Failure<WeatherRecordsPage>(new Error($"{nameof(WeatherService)}.{nameof(GetRecordsPageAsync)}", "Weather archive not found."));
        }
        
        var totalRecordsCount = context
            .WeatherRecords
            .AsNoTracking()
            .Where(e => e.WeatherArchiveId == weatherArchiveId)
            .ApplyFiltering(filteringOptions)
            .Count();

        var records = await context
            .WeatherRecords
            .Include(e => e.WeatherArchive)
            .AsNoTracking()
            .Where(e => e.WeatherArchiveId == weatherArchiveId)
            .OrderBy(e => e.MeasurementDate)
            .ApplyFiltering(filteringOptions)
            .ApplyPaging(pagingOptions)
            .ToListAsync(cancellationToken);

        return new WeatherRecordsPage(records.Select(e => e.ToDTO()).ToList(), totalRecordsCount, weatherArchive.ToDTO(), pagingOptions);
    }
}
