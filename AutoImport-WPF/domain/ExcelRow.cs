using NPOI.SS.UserModel;

namespace AutoImport_WPF.domain;

public class ExcelRow(IRow row)
{
    public string this[int index] => row.GetCell(index).ToString() ?? string.Empty;

    public double GetNumberValue(int index)
    {
        return row.GetCell(index).NumericCellValue;
    }
}