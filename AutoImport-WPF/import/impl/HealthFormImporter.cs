using AutoImport_WPF.browser;
using AutoImport_WPF.config;
using AutoImport_WPF.domain;
using AutoImport_WPF.excel;
using AutoImport_WPF.log;

namespace AutoImport_WPF.import.impl;

public class HealthFormImporter : IFileImport, IListDataImport<HealthFormData>
{
    private static ILogger Logger => LogConfig.Logger;

    private static IBrowser Browser => BrowserConfig.Browser;

    public void Import(string filename)
    {
        var healthFormDataList = ReadFromExcelFile(filename);
        Import(healthFormDataList);
    }

    public void Import(List<HealthFormData> dataList)
    {
        throw new NotImplementedException();
    }

    private static List<HealthFormData> ReadFromExcelFile(string fileName)
    {
        return new HealthFormExcelReader().Read(fileName);
    }
}