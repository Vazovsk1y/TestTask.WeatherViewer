namespace TestTask.Application.Shared;

public class WeatherRecordDTO
{
	public DateOnly MeasurementDate { get; set; }

	public TimeOnly MeasurementTime { get; set; }

	public double AirTemperature { get; set; }

	public double AirHumidity { get; set; }

	public double DewPoint { get; set; }

	public double AirPressure { get; set; }

	public string? WindDirections { get; set; }

	public double? WindSpeed { get; set; }

	public double? Clouds { get; set; }

	public double? LowCloudBoundary { get; set; }

	public double? HorizontalVisibility { get; set; }

	public string? NaturalPhenomena { get; set; }
}
