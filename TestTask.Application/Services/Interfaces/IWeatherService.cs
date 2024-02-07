using TestTask.Application.Contracts;

namespace TestTask.Application.Services.Interfaces;

public interface IWeatherService
{
    Task<Response> SaveFromFileAsync(string filePath, CancellationToken cancellationToken = default);

    Task<Response<WeatherRecordsPage>> GetAsync(
        PagingOptions? pagingOptions = null,
        WeatherRecordsFilteringOptions? filteringOptions = null,
        CancellationToken cancellationToken = default);
}

