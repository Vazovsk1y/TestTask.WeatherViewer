namespace TestTask.Application.Contracts;

public record WeatherRecordsPage : Page<WeatherRecordDTO>
{
    public WeatherArchiveDTO WeatherArchive { get; }
    public WeatherRecordsPage(IReadOnlyCollection<WeatherRecordDTO> items, int totalItemsItemsCount, WeatherArchiveDTO weatherArchive, PagingOptions? pagingOptions = null) : base(items, totalItemsItemsCount, pagingOptions)
    {
        WeatherArchive = weatherArchive;
    }
}