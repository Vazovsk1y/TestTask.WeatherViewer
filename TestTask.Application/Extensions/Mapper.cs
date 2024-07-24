using TestTask.Application.Contracts;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Extensions;

public static class Mapper
{
	public static WeatherRecordDTO ToDTO(this WeatherRecord record)
	{
		return new WeatherRecordDTO
		{
			MeasurementDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(record.MeasurementDate, record.WeatherArchive.LocalityTimeZoneId),
			AirHumidity = record.AirHumidity,
			AirTemperature = record.AirTemperature,
			AirPressure = record.AirPressure,
			Clouds = record.Clouds,
			DewPoint = record.DewPoint,
			HorizontalVisibility = record.HorizontalVisibility,
			LowCloudBoundary = record.LowCloudBoundary,
			WeatherPhenomena = record.WeatherPhenomena,
			WindSpeed = record.WindSpeed,
			WindDirection = record.WindDirection.ToString()
		};
	}

	public static WeatherArchiveDTO ToDTO(this WeatherArchive weatherArchive)
	{
		return new WeatherArchiveDTO(
			weatherArchive.Id,
			weatherArchive.DateAdded,
			weatherArchive.Title,
			weatherArchive.LocalityTitle,
			TimeZoneInfo.FindSystemTimeZoneById(weatherArchive.LocalityTimeZoneId),
			weatherArchive.WeatherRecords.Count()
		);
	}

	public static WeatherRecord ToRecord(this WeatherRecordAddDTO dto, WeatherArchive parent)
	{
		return new WeatherRecord()
		{
			WeatherArchiveId = parent.Id,
			AirPressure = dto.AirPressure,
			DewPoint = dto.DewPoint,
			AirTemperature = dto.AirTemperature,
			AirHumidity = dto.AirHumidity,
			MeasurementDate = TimeZoneInfo.ConvertTimeToUtc(new DateTime(dto.MeasurementDate, dto.MeasurementTime), TimeZoneInfo.FindSystemTimeZoneById(parent.LocalityTimeZoneId)),
			WeatherPhenomena = dto.WeatherPhenomena,
			WindDirection = dto.WindDirection,
			WindSpeed = dto.WindSpeed,
			Clouds = dto.Clouds,
			LowCloudBoundary = dto.LowCloudBoundary,
			HorizontalVisibility = dto.HorizontalVisibility
		};
	}
}
