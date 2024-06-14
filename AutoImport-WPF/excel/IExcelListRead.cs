using AutoImport_WPF.domain;

namespace AutoImport_WPF.excel;

public interface IExcelListRead<T> where T : ExcelRowData
{
    List<T> Read(string fileName);
}