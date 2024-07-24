using TestTask.Application.Contracts;
using TestTask.Web.Pages.Weather.Archives;
using TestTask.Web.ViewModels;

namespace TestTask.Web.Extensions;

public static class Mapper
{
    public static WeatherArchiveViewModel ToViewModel(this WeatherArchiveDTO dto)
    {
        return new WeatherArchiveViewModel(
            dto.Id,
            dto.DateAdded,
            dto.Title,
            $"{dto.LocalityTitle}+{dto.LocalityTimeZone.BaseUtcOffset:hh\\:mm}",
            dto.RecordsCount
        );
    }

    public static WeatherRecordViewModel ToViewModel(this WeatherRecordDTO dto)
    {
        return new WeatherRecordViewModel()
        {
            MeasurementDate = DateOnly.FromDateTime(dto.MeasurementDate.Date),
            MeasurementTime = TimeOnly.FromDateTime(dto.MeasurementDate.DateTime),
            AirHumidity = dto.AirHumidity,
            AirPressure = dto.AirPressure,
            AirTemperature = dto.AirTemperature,
            Clouds = dto.Clouds,
            DewPoint = dto.DewPoint,
            HorizontalVisibility = dto.HorizontalVisibility,
            LowCloudBoundary = dto.LowCloudBoundary,
            WeatherPhenomena = dto.WeatherPhenomena,
            WindDirection = dto.WindDirection,
            WindSpeed = dto.WindSpeed
        };
    }
}