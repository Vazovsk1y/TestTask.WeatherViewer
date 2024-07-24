namespace TestTask.DAL.PostgreSQL.Models;

public class WeatherRecord
{
	public Guid Id { get; } = Guid.NewGuid();
	
	public required Guid WeatherArchiveId { get; init; }

	/// <summary>
	/// Время и дата сбора информации.
	/// </summary>
	public required DateTimeOffset MeasurementDate { get; init; }

	/// <summary>
	/// Температура воздуха, °C.
	/// </summary>
	public required double AirTemperature { get; init; } 

	/// <summary>
	/// Влажность воздуха, %.
	/// </summary>
	public required double AirHumidity { get; init; }

	/// <summary>
	/// Точка росы, °C.
	/// </summary>
	public required double DewPoint { get; init; }

	/// <summary>
	/// Атмосферное давление, мм рт. ст.
	/// </summary>
	public required double AirPressure { get; init; }

	/// <summary>
	/// Скорость ветра, м/с.
	/// </summary>
	public required double? WindSpeed { get; init; }

	/// <summary>
	/// Облачность, %.
	/// </summary>
	public required double? Clouds { get; init; }

	/// <summary>
	/// Нижняя граница облачности, м.
	/// </summary>
	public required double? LowCloudBoundary { get; init; }

	/// <summary>
	/// Горизонтальная видимость, км.
	/// </summary>
	public required double? HorizontalVisibility { get; init; }

	/// <summary>
	/// Погодные явления.
	/// </summary>
	public required string? WeatherPhenomena { get; init; }

	/// <summary>
	/// Направление ветра.
	/// </summary>
	public required WindDirections WindDirection { get; init; }

	#region --Navigation--

	public WeatherArchive WeatherArchive { get; init; } = null!;

	#endregion
}