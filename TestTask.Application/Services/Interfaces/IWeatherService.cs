using TestTask.Application.Contracts;

namespace TestTask.Application.Services.Interfaces;

public interface IWeatherService
{
    Task<Response> AddArchiveAsync(
        WeatherArchiveAddDTO dto, 
        CancellationToken cancellationToken = default);

    Task<Response<Page<WeatherArchiveDTO>>> GetArchivesPageAsync(
        PagingOptions? pagingOptions = null,
        CancellationToken cancellationToken = default);
    
    Task<Response<WeatherRecordsPage>> GetRecordsPageAsync(
        Guid weatherArchiveId,
        PagingOptions? pagingOptions = null,
        WeatherRecordsFilteringOptions? filteringOptions = null,
        CancellationToken cancellationToken = default);
}

