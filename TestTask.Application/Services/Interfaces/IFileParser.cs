namespace TestTask.Application.Interfaces;

public interface IFileParser<T>
{
	IEnumerable<T> GetFromFile(string filePath);
}
