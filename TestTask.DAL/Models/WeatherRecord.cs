namespace TestTask.DAL.Models;

public class WeatherRecord
{
	public Guid Id { get; } 

	/// <summary>
	/// Время и дата замера данных, время по мск.
	/// </summary>
	public DateTime MeasurementDate { get; set; }

	/// <summary>
	/// Температура воздуха, °C
	/// </summary>
	public double AirTemperature { get; set; } 

	/// <summary>
	/// Влажность воздуха, %
	/// </summary>
	public double AirHumidity { get; set; }

	/// <summary>
	/// Точка росы, °C
	/// </summary>
	public double DewPoint { get; set; }

	/// <summary>
	/// Атмосферное давление, мм рт. ст.
	/// </summary>
	public double AirPressure { get; set; }

	/// <summary>
	/// Скорость ветра, м/с
	/// </summary>
	public double? WindSpeed { get; set; }

	/// <summary>
	/// Облачность, %
	/// </summary>
	public double? Clouds { get; set; }

	/// <summary>
	/// Нижняя граница облачности, м
	/// </summary>
	public double LowCloudBoundary { get; set; }

	/// <summary>
	/// Горизонтальная видимость, км
	/// </summary>
	public double? HorizontalVisibility { get; set; }

	/// <summary>
	/// Природные явления
	/// </summary>
	public string? NaturalPhenomena { get; set; }

	/// <summary>
	/// Главное направление ветра Юг, Север, Восток, Запад
	/// </summary>
	public WindDirection MainWindDirection { get; set; }

	/// <summary>
	/// Дополнительное направление ветра Юго-восток, Северо-запад, Юго-запад, Северо-восток
	/// </summary>
	public WindDirection SecondaryWindDirection { get; set; }

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
