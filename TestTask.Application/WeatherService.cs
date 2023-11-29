using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.Application.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL;
using TestTask.DAL.Models;

namespace TestTask.Application;

internal class WeatherService : IWeatherService
{
	private readonly TestTaskSecondContext _context;
	private readonly ILogger<WeatherService> _logger;
	private readonly ITableProvider<WeatherRecord> _tableProvider;

	public WeatherService(
		TestTaskSecondContext context, 
		ILogger<WeatherService> logger, 
		ITableProvider<WeatherRecord> tableProvider)
	{
		_context = context;
		_logger = logger;
		_tableProvider = tableProvider;
	}

	public async Task<Response<WeatherPage>> GetAsync(PagingOptions? pagingOptions = null, FilterOptions? filter = null, CancellationToken cancellationToken = default)
	{
		int totalItemsCount = _context.WeatherRecords.Count();
		int pageIndex = pagingOptions is null ? 1 : pagingOptions.PageIndex;
		int pageSize = pagingOptions is null ? totalItemsCount : pagingOptions.PageSize;
		if (pageSize > totalItemsCount)
		{
			pageSize = totalItemsCount;
		}

		var query = _context.WeatherRecords.AsNoTracking();

		if (filter is not null)
		{
			if (filter.ByMonth != Months.None)
			{
				query = query.Where(e => e.MeasurementDate.Month == (int)filter!.ByMonth);
			}

			if (filter.ByYear is not null)
			{
				query = query.Where(e => e.MeasurementDate.Year == filter.ByYear);
			}
		}

		var pagedQuery =
			query
			.OrderBy(e => e.MeasurementDate)
			.Skip((pageIndex - 1) * pageSize)
			.Take(pageSize);

		return Response.Success(new WeatherPage(await pagedQuery.Select(e => e.ToDTO()).ToListAsync(cancellationToken), totalItemsCount, pageIndex, pageSize));
	}

	public async Task<Response> SaveFromTableAsync(string tableFilePath, CancellationToken cancellationToken = default)
	{
		try
		{
			await _context.WeatherRecords.AddRangeAsync(_tableProvider.GetFrom(tableFilePath), cancellationToken);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Something went wrong");
			return Response.Failure(ex.Message);
		}

		await _context.SaveChangesAsync(cancellationToken);
		return Response.Success();
	}
}
