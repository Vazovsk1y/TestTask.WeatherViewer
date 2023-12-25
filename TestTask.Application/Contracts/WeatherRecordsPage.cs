namespace TestTask.Application.Contracts;

public record WeatherRecordsPage : Page<WeatherRecordDTO>
{
	public WeatherRecordsPage(IReadOnlyCollection<WeatherRecordDTO> items, int totalItemsCount, PagingOptions? pagingOptions = null) : base(items, totalItemsCount, pagingOptions)
	{
	}
}