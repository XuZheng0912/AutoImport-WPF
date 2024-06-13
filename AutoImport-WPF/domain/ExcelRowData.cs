namespace AutoImport_WPF.domain;

public abstract class ExcelRowData(ExcelRow row)
{
    protected string this[int index] => row[index];
}