namespace TestTask.DAL.Models;

public class WeatherRecord
{
	public Guid Id { get; } 

	/// <summary>
	/// Время и дата замера данных.
	/// </summary>
	public required DateTimeOffset MeasurementDate { get; init; }

	/// <summary>
	/// Температура воздуха, °C
	/// </summary>
	public required double AirTemperature { get; init; } 

	/// <summary>
	/// Влажность воздуха, %
	/// </summary>
	public required double AirHumidity { get; init; }

	/// <summary>
	/// Точка росы, °C
	/// </summary>
	public required double DewPoint { get; init; }

	/// <summary>
	/// Атмосферное давление, мм рт. ст.
	/// </summary>
	public required double AirPressure { get; init; }

	/// <summary>
	/// Скорость ветра, м/с
	/// </summary>
	public double? WindSpeed { get; init; }

	/// <summary>
	/// Облачность, %
	/// </summary>
	public double? Clouds { get; init; }

	/// <summary>
	/// Нижняя граница облачности, м
	/// </summary>
	public double? LowCloudBoundary { get; init; }

	/// <summary>
	/// Горизонтальная видимость, км
	/// </summary>
	public double? HorizontalVisibility { get; init; }

	/// <summary>
	/// Природные явления
	/// </summary>
	public string? NaturalPhenomena { get; init; }

	/// <summary>
	/// Главное направление ветра Юг, Север, Восток, Запад
	/// </summary>
	public required WindDirection MainWindDirection { get; init; }

	/// <summary>
	/// Дополнительное направление ветра Юго-восток, Северо-запад, Юго-запад, Северо-восток
	/// </summary>
	public required WindDirection SecondaryWindDirection { get; init; }

	public WeatherRecord() 
	{
		Id = Guid.NewGuid();
	}
}

public enum WindDirection
{
	Undefined,
	Calm,
	North,
	NorthEast,
	East,
	SouthEast,
	South,
	SouthWest,
	West,
	NorthWest
}
