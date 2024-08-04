using System.Globalization;
using NPOI.SS.UserModel;

namespace AutoImport_WPF.domain;

public class ExcelRow(IRow row)
{
    public string this[int index] => GetCellValueAsString(row.GetCell(index)) ?? string.Empty;
    
    private static string? GetCellValueAsString(ICell cell)
    {
        return cell.CellType switch
        {
            CellType.String => cell.StringCellValue,
            CellType.Numeric => DateUtil.IsCellDateFormatted(cell)
                ? cell.DateCellValue.ToString()
                : cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
            CellType.Boolean => cell.BooleanCellValue.ToString(),
            CellType.Formula => cell.CachedFormulaResultType switch
            {
                CellType.String => cell.StringCellValue,
                CellType.Numeric => cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                _ => cell.ToString()
            },
            _ => cell.ToString()
        };
    }
    
    public double GetNumberValue(int index)
    {
        return row.GetCell(index).NumericCellValue;
    }
}