namespace TestTask.Application.Interfaces;

public interface ITableProvider<T>
{
	IEnumerable<T> GetFrom(string tableFilePath);
}
