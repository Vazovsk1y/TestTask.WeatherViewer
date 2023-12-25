using TestTask.Application.Contracts;

namespace TestTask.Application.Interfaces;

public interface IWeatherService
{
	Task<Response> SaveFromTableAsync(string tableFilePath, CancellationToken cancellationToken = default);

	Task<Response<WeatherRecordsPage>> GetAsync(
		PagingOptions? pagingOptions = null, 
		WeatherRecordsFilteringOptions? filteringOptions = null, 
		CancellationToken cancellationToken = default);
}

