using AutoImport_WPF.domain;

namespace AutoImport_WPF.excel;

public interface IExcelListRead<T>
{
    List<T> Read(string fileName);
}