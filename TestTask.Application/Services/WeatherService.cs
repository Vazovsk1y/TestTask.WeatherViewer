using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.Application.Interfaces;
using TestTask.Application.Contracts;
using TestTask.DAL;
using TestTask.DAL.Models;
using TestTask.Application.Services.Interfaces;

namespace TestTask.Application.Services;

internal class WeatherService : IWeatherService
{
    private readonly TestTaskDbContext _context;
    private readonly ILogger<WeatherService> _logger;
    private readonly IFileParser<WeatherRecord> _fileParser;

    public WeatherService(
        TestTaskDbContext context,
        ILogger<WeatherService> logger,
        IFileParser<WeatherRecord> tableProvider)
    {
        _context = context;
        _logger = logger;
        _fileParser = tableProvider;
    }

    public async Task<Response<WeatherRecordsPage>> GetAsync(PagingOptions? pagingOptions = null, WeatherRecordsFilteringOptions? filteringOptions = null, CancellationToken cancellationToken = default)
    {
        var query = _context
            .WeatherRecords
            .AsNoTracking()
            .ApplyFiltering(filteringOptions);

        var totalCount = query.Count();
        var result = await
            _context
            .WeatherRecords
            .AsNoTracking()
            .OrderBy(e => e.MeasurementDate)
            .ApplyFiltering(filteringOptions)
            .ApplyPaging(pagingOptions)
            .Select(e => e.ToDTO())
            .ToListAsync(cancellationToken);

        return new WeatherRecordsPage(result, totalCount, pagingOptions);
    }

    public async Task<Response> SaveFromFileAsync(string tableFilePath, CancellationToken cancellationToken = default)
    {
        try
        {
            var records = _fileParser.GetFromFile(tableFilePath);

            _context.WeatherRecords.AddRange(records);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while trying to save from file.");
            return Response.Failure(ex.Message);
        }

        return Response.Success();
    }
}
