namespace TestTask.Application.Shared;

public record WeatherPage : Page<WeatherRecordDTO>
{
	public WeatherPage(IReadOnlyCollection<WeatherRecordDTO> items, int totalItemsCount, int pageIndex, int pageSize) : base(items, totalItemsCount, pageIndex, pageSize)
	{
	}
}