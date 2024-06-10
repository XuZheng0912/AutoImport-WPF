using AutoImport_WPF.config;
using AutoImport_WPF.domain;

namespace AutoImport_WPF.service.impl;

public class ImportService : IImportService
{
    private readonly IExcelReadService _excelReadService = new PoiExcelReadService();

    private IBrowser Browser { get; } = new ChromeBrowser();

    public void Import(string fileName)
    {
        var physicalExaminationDataList = ReadFromExcel(fileName);
        Browser.Get(WebConfig.TargetUrl);
        Import(physicalExaminationDataList);
    }

    public void Import(List<PhysicalExaminationData> dataList)
    {
        foreach (var physicalExaminationData in dataList)
        {
            Import(physicalExaminationData);
        }
    }

    private static void Import(PhysicalExaminationData data)
    {
    }

    private List<PhysicalExaminationData> ReadFromExcel(string fileName)
    {
        return _excelReadService.Read(fileName);
    }
}