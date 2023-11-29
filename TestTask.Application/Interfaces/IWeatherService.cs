using TestTask.Application.Shared;

namespace TestTask.Application.Interfaces;

public interface IWeatherService
{
	Task<Response> SaveFromTableAsync(string tableFilePath, CancellationToken cancellationToken = default);

	Task<Response<WeatherPage>> GetAsync(PagingOptions? pagingOptions = null, FilterOptions? filter = null, CancellationToken cancellationToken = default);
}

