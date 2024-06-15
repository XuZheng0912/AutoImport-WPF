using AutoImport_WPF.service.impl;

namespace AutoImport_WPF.service;

public static class ImportServiceProvider
{
    public static IImportService GetImportService()
    {
        return new ImportService();
    }
}