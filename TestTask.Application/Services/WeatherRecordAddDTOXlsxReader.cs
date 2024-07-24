using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

public class WeatherRecordAddDTOXlsxReader : IFileReader<WeatherRecordAddDTO>
{
    private const int ColumnsCount = 12;

    private const int MeasurementDateIndex = 0;

    private const int MeasurementTimeIndex = 1;

    private const int AirTemperatureIndex = 2;

    private const int AirHumidityIndex = 3;

    private const int DewPointIndex = 4;

    private const int AirPressureIndex = 5;

    private const int WindDirectionIndex = 6;

    private const int WindSpeedIndex = 7;

    private const int CloudsIndex = 8;

    private const int LowCloudBoundaryIndex = 9;

    private const int HorizontalVisibilityIndex = 10;

    private const int WeatherPhenomenaIndex = 11;

    private const int RowsSkipCount = 4;
    
    private static readonly Dictionary<string, WindDirections> WindDirections = new()
    {
        { "С", DAL.PostgreSQL.Models.WindDirections.North },
        { "Ю", DAL.PostgreSQL.Models.WindDirections.South },
        { "З", DAL.PostgreSQL.Models.WindDirections.West },
        { "В", DAL.PostgreSQL.Models.WindDirections.East },
        { "СЗ", DAL.PostgreSQL.Models.WindDirections.NorthWest },
        { "СВ", DAL.PostgreSQL.Models.WindDirections.NorthEast },
        { "ЮЗ", DAL.PostgreSQL.Models.WindDirections.SouthWest },
        { "ЮВ", DAL.PostgreSQL.Models.WindDirections.SouthEast },
        { "штиль", DAL.PostgreSQL.Models.WindDirections.Calm },
    };
    
    public IEnumerable<WeatherRecordAddDTO> Enumerate(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        using var workbook = new XSSFWorkbook(fileInfo.OpenRead());

        for (var i = 0; i < workbook.NumberOfSheets; i++)
        {
            var currentSheet = workbook.GetSheetAt(i);
            for (var j = RowsSkipCount; j < currentSheet.PhysicalNumberOfRows; j++)
            {
                var row = currentSheet.GetRow(j);
                var cells = row.Cells;

                var missingCellsCount = ColumnsCount - cells.Count;
                var newCellIndex = ColumnsCount - missingCellsCount;
                while (cells.Count < ColumnsCount)
                {
                    cells.Add(row.CreateCell(newCellIndex));
                    newCellIndex++;
                }

                yield return new WeatherRecordAddDTO(
                    DateOnly.Parse(cells[MeasurementDateIndex].StringCellValue),
                    TimeOnly.Parse(cells[MeasurementTimeIndex].StringCellValue),
                    cells[AirTemperatureIndex].NumericCellValue,
                    cells[AirHumidityIndex].NumericCellValue,
                    cells[DewPointIndex].NumericCellValue,
                    cells[AirPressureIndex].NumericCellValue,
                    ParseNullableDouble(cells[WindSpeedIndex]),
                    ParseNullableDouble(cells[CloudsIndex]),
                    ParseNullableDouble(cells[LowCloudBoundaryIndex]),
                    ParseNullableDouble(cells[HorizontalVisibilityIndex]),
                    ParseNullableString(cells[WeatherPhenomenaIndex]),
                    ParseNullableString(cells[WindDirectionIndex]) is not { } wd ? DAL.PostgreSQL.Models.WindDirections.Undefined : WindDirections[wd]
                );
            }
        }
    }
    
    private static string? ParseNullableString(ICell cell)
    {
        return cell.CellType == CellType.String && string.IsNullOrWhiteSpace(cell.StringCellValue) ? null : cell.StringCellValue;
    }

    private static double? ParseNullableDouble(ICell cell)
    {
        return cell.CellType != CellType.Numeric ? null : cell.NumericCellValue;
    }
}