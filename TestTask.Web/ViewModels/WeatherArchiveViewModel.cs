namespace TestTask.Web.ViewModels;

public record WeatherArchiveViewModel(
    Guid Id,
    DateTimeOffset DateAdded,
    string Title,
    string LocalityInfo,
    int RecordsCount);