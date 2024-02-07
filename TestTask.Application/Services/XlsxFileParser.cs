using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TestTask.Application.Interfaces;
using TestTask.DAL.Models;

namespace TestTask.Application.Services;

public class XlsxFileParser : IFileParser<WeatherRecord>
{
    private static readonly Dictionary<string, WindDirection> _windDirections = new()
    {
        { "С", WindDirection.North },
        { "Ю", WindDirection.South },
        { "З", WindDirection.West },
        { "В", WindDirection.East },
        { "СЗ", WindDirection.NorthWest },
        { "СВ", WindDirection.NorthEast },
        { "ЮЗ", WindDirection.SouthWest },
        { "ЮВ", WindDirection.SouthEast },
        { "штиль", WindDirection.Calm },
    };

    public IEnumerable<WeatherRecord> GetFromFile(string tableFilePath)
    {
        var fileInfo = new FileInfo(tableFilePath);
        using var workbook = new XSSFWorkbook(fileInfo.OpenRead());

        var sheets = new List<ISheet>();
        for (int i = 0; i < workbook.NumberOfSheets; i++)
        {
            sheets.Add(workbook.GetSheetAt(i));
        }

        foreach (var sheet in sheets)
        {
            foreach (var row in MoscowWeatherArchiveFile.GetRows(sheet))
            {
                yield return ToWeatherRecord(row);
            }
        }
    }

    private static WeatherRecord ToWeatherRecord(MoscowWeatherArchiveFile.Row row)
    {
        var (main, secondary) = ToWindDirectrions(row.WindDirections);
        return new WeatherRecord
        {
            AirHumidity = row.AirHumidity,
            AirPressure = row.AirPressure,
            AirTemperature = row.AirTemperature,
            Clouds = row.Clouds,
            LowCloudBoundary = row.LowCloudBoundary,
            DewPoint = row.DewPoint,
            HorizontalVisibility = row.HorizontalVisibility,
            NaturalPhenomena = row.NaturalPhenomena,
            WindSpeed = row.WindSpeed,
            MeasurementDate = new DateTimeOffset(
                row.MeasurementDate.Year, 
                row.MeasurementDate.Month, 
                row.MeasurementDate.Day, 
                row.MeasurementTime.Hour, 
                row.MeasurementTime.Minute, 
                row.MeasurementTime.Second, 
                MoscowWeatherArchiveFile.TimeOffset),
            MainWindDirection = main,
            SecondaryWindDirection = secondary,
        };
    }

    private static (WindDirection main, WindDirection secondary) ToWindDirectrions(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return (WindDirection.Undefined, WindDirection.Undefined);
        }

        var directions = value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        return directions.Count switch
        {
            1 => (_windDirections[directions[0]], WindDirection.Undefined),
            2 => (_windDirections[directions[0]], _windDirections[directions[1]]),
            _ => (WindDirection.Undefined, WindDirection.Undefined),
        };
    }
}