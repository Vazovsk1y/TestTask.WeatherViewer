namespace TestTask.Application.Services.Interfaces;

public interface IFileReader<out T>
{
	IEnumerable<T> Enumerate(string filePath);
}
