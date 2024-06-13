using AutoImport_WPF.domain;

namespace AutoImport_WPF.service;

public interface IExcelReadService
{
    List<string> Read(string filePath);
}