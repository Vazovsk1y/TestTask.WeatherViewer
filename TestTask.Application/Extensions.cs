using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TestTask.Application.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.Models;

namespace TestTask.Application;

public static class Extensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddTransient<ITableProvider<WeatherRecord>, XlsxWeatherTableProvider>()
		.AddScoped<IWeatherService, WeatherService>();

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
