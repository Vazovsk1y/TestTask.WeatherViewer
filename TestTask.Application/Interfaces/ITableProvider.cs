using TestTask.DAL.Models;

namespace TestTask.Application.Interfaces;

public interface ITableProvider
{
	Task<IReadOnlyCollection<WeatherRecord>> ParseFromAsync(string tableFilePath, CancellationToken cancellationToken = default);
}
