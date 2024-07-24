namespace TestTask.DAL.PostgreSQL.Models;

public class WeatherArchive
{
    public Guid Id { get; } = Guid.NewGuid();

    public required DateTimeOffset DateAdded { get; init; }
    
    /// <summary>
    /// Название архива.
    /// </summary>
    public required string Title { get; init; }
	
    /// <summary>
    /// Название целевого населенного пункта.
    /// </summary>
    public required string LocalityTitle { get; init; }
	
    /// <summary>
    /// Идентификатор временной зоны для целевого населенного пункта.
    /// </summary>
    public required string LocalityTimeZoneId { get; init; }

    #region --Navigation--

    public IEnumerable<WeatherRecord> WeatherRecords { get; init; } = new List<WeatherRecord>();

    #endregion
}