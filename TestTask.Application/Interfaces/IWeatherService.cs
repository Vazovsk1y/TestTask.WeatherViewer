using TestTask.Application.Shared;

namespace TestTask.Application.Interfaces;

public interface IWeatherService
{
	Task<Response> SaveFromTableAsync(string tableFilePath, CancellationToken cancellationToken = default);

	Task<Response<IReadOnlyCollection<WeatherRecordDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
}
