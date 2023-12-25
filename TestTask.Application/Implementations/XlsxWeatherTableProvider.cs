using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TestTask.Application.Interfaces;
using TestTask.DAL.Models;

namespace TestTask.Application.Implementations;

public class XlsxWeatherTableProvider : ITableProvider<WeatherRecord>
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

    public IEnumerable<WeatherRecord> GetFrom(string tableFilePath)
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
            foreach (var row in MoscowWeatherTableParser.GetRows(sheet))
            {
                yield return ToWeatherRecord(row);
            }
        }
    }

    private static WeatherRecord ToWeatherRecord(MoscowWeatherTableParser.Row row)
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
            MeasurementDate = new DateTime(row.MeasurementDate.Year, row.MeasurementDate.Month, row.MeasurementDate.Day, row.MeasurementTime.Hour, row.MeasurementTime.Minute, row.MeasurementTime.Second),
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

internal class MoscowWeatherTableParser
{
    public const int SheetsCount = 12;

    public const int ColumnsCount = 12;

    public const int MeasurementDateIndex = 0;

    public const int MeasurementTimeIndex = 1;

    public const int AirTemperatureIndex = 2;

    public const int AirHumidityIndex = 3;

    public const int DewPointIndex = 4;

    public const int AirPressureIndex = 5;

    public const int WindDirectionsIndex = 6;

    public const int WindSpeedIndex = 7;

    public const int CloudsIndex = 8;

    public const int LowCloudBoundaryIndex = 9;

    public const int HorizontalVisibilityIndex = 10;

    public const int NaturalPhenomenaIndex = 11;

    public static IEnumerable<Row> GetRows(ISheet sheet)
    {
        int rowsCount = sheet.PhysicalNumberOfRows;
        for (int i = 4; i < rowsCount; i++)                        // start from fourth row to skip sheet info before first actual information starts
        {
            var row = sheet.GetRow(i);
            var cells = row.Cells;

            int missingCellsCount = ColumnsCount - cells.Count;
            int index = ColumnsCount - missingCellsCount;
            while (cells.Count < ColumnsCount)                     // if cells count lower than required we fill until it will be required count
            {
                cells.Add(row.CreateCell(index));
                index++;
            }

            yield return new Row
            {
                MeasurementDate = DateOnly.Parse(cells[MeasurementDateIndex].StringCellValue),
                MeasurementTime = TimeOnly.Parse(cells[MeasurementTimeIndex].StringCellValue),
                AirTemperature = cells[AirTemperatureIndex].NumericCellValue,
                AirHumidity = cells[AirHumidityIndex].NumericCellValue,
                DewPoint = cells[DewPointIndex].NumericCellValue,
                AirPressure = cells[AirPressureIndex].NumericCellValue,
                WindDirections = ParseNullableString(cells[WindDirectionsIndex]),
                WindSpeed = ParseNullableDouble(cells[WindSpeedIndex]),
                Clouds = ParseNullableDouble(cells[CloudsIndex]),
                LowCloudBoundary = ParseNullableDouble(cells[LowCloudBoundaryIndex]),
                HorizontalVisibility = ParseNullableDouble(cells[HorizontalVisibilityIndex]),
                NaturalPhenomena = ParseNullableString(cells[NaturalPhenomenaIndex]),
            };
        }
    }

    private static string? ParseNullableString(ICell cell)
    {
        return cell.CellType == CellType.String && string.IsNullOrEmpty(cell.StringCellValue) ? null : cell.StringCellValue;
    }

    private static double? ParseNullableDouble(ICell cell)
    {
        return cell.CellType != CellType.Numeric ? null : cell.NumericCellValue;
    }

    internal class Row
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
}
