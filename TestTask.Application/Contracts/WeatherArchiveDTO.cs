namespace TestTask.Application.Contracts;

public record WeatherArchiveDTO(
    Guid Id,
    DateTimeOffset DateAdded,
    string Title,
    string LocalityTitle,
    TimeZoneInfo LocalityTimeZone,
    int RecordsCount);