using TestTask.Application.Contracts;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Extensions;

public static class IQueryableEx
{
	public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> collection, PagingOptions? pagingOptions)
	{
		if (pagingOptions is null)
		{
			return collection;
		}
		
		switch (pagingOptions)
		{
			case { PageIndex: <= 0 }:
				throw new ArgumentException("Page index must be greater than zero.");
			case { PageSize: <= 0 }:
				throw new ArgumentException("Page size must be greater than zero.");
		}

		return collection
			.Skip((pagingOptions.PageIndex - 1) * pagingOptions.PageSize)
			.Take(pagingOptions.PageSize);
	}

	public static IQueryable<WeatherRecord> ApplyFiltering(this IQueryable<WeatherRecord> weatherRecords, WeatherRecordsFilteringOptions? filteringOptions)
	{
		if (filteringOptions is null)
		{
			return weatherRecords;
		}

		if (filteringOptions.ByMonth != Months.None)
		{
			weatherRecords = weatherRecords.Where(e => e.MeasurementDate.Month == (int)filteringOptions.ByMonth);
		}

		if (filteringOptions.ByYear is not null)
		{
			weatherRecords = weatherRecords.Where(e => e.MeasurementDate.Year == filteringOptions.ByYear);
		}

		return weatherRecords;
	}
}
