using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.Application.Interfaces;
using TestTask.Application.Contracts;
using TestTask.DAL;
using TestTask.DAL.Models;

namespace TestTask.Application.Implementations;

internal class WeatherService : IWeatherService
{
    private readonly TestTaskDbContext _context;
    private readonly ILogger<WeatherService> _logger;
    private readonly ITableProvider<WeatherRecord> _tableProvider;

    public WeatherService(
        TestTaskDbContext context,
        ILogger<WeatherService> logger,
        ITableProvider<WeatherRecord> tableProvider)
    {
        _context = context;
        _logger = logger;
        _tableProvider = tableProvider;
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
            .ApplyFiltering(filteringOptions)
            .ApplyPaging(pagingOptions)
            .Select(e => e.ToDTO())
            .ToListAsync(cancellationToken);

        return new WeatherRecordsPage(result, totalCount, pagingOptions);
    }

    public async Task<Response> SaveFromTableAsync(string tableFilePath, CancellationToken cancellationToken = default)
    {
        try
        {
            var records = _tableProvider.GetFrom(tableFilePath);
			await _context.WeatherRecords.AddRangeAsync(records, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while trying to save from table.");
            return Response.Failure(ex.Message);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Response.Success();
    }
}
