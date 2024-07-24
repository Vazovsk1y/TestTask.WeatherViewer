using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Contracts;

public record WeatherRecordAddDTO(
    DateOnly MeasurementDate,
    TimeOnly MeasurementTime,
    double AirTemperature,
    double AirHumidity,
    double DewPoint,
    double AirPressure,
    double? WindSpeed,
    double? Clouds,
    double? LowCloudBoundary,
    double? HorizontalVisibility,
    string? WeatherPhenomena,
    WindDirections WindDirection);