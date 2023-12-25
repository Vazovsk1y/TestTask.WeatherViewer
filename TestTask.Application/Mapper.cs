using TestTask.Application.Contracts;
using TestTask.DAL.Models;

namespace TestTask.Application;

public static class Mapper
{
	public static WeatherRecordDTO ToDTO(this WeatherRecord record)
	{
		return new WeatherRecordDTO
		{
			MeasurementDate = new DateOnly(record.MeasurementDate.Year, record.MeasurementDate.Month, record.MeasurementDate.Day),
			MeasurementTime = new TimeOnly(record.MeasurementDate.Hour, record.MeasurementDate.Minute),
			AirHumidity = record.AirHumidity,
			AirTemperature = record.AirTemperature,
			AirPressure = record.AirPressure,
			Clouds = record.Clouds,
			DewPoint = record.DewPoint,
			HorizontalVisibility = record.HorizontalVisibility,
			LowCloudBoundary = record.LowCloudBoundary,
			NaturalPhenomena = record.NaturalPhenomena,
			WindSpeed = record.WindSpeed,
			WindDirections = ToWindDirectionsString(record)
		};
	}

	private static string? ToWindDirectionsString(WeatherRecord record)
	{
		if (record.MainWindDirection == WindDirection.Undefined && record.SecondaryWindDirection == WindDirection.Undefined)
		{
			return null;
		}

		return record.SecondaryWindDirection == WindDirection.Undefined ? $"{record.MainWindDirection}" : $"{record.MainWindDirection},{record.SecondaryWindDirection}";
	}
}
