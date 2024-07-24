namespace TestTask.Web.ViewModels;

public record WeatherRecordViewModel
{
    public required DateOnly MeasurementDate { get; init; }
    
    public required TimeOnly MeasurementTime { get; init; }

    public required double AirTemperature { get; init; }

    public required double AirHumidity { get; init; }

    public required double DewPoint { get; init; }

    public required double AirPressure { get; init; }

    public required string? WindDirection { get; init; }

    public required double? WindSpeed { get; init; }

    public required double? Clouds { get; init; }

    public required double? LowCloudBoundary { get; init; }

    public required double? HorizontalVisibility { get; init; }

    public required string? WeatherPhenomena { get; init; }
}