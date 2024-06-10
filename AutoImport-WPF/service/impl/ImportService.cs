using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.domain;
using OpenQA.Selenium;

namespace AutoImport_WPF.service.impl;

public class ImportService : IImportService
{
    private readonly IExcelReadService _excelReadService = new PoiExcelReadService();

    private IBrowser Browser { get; } = new ChromeBrowser();

    public void Import(string fileName)
    {
        var physicalExaminationDataList = ReadFromExcel(fileName);
        OpenWebsite();
        Login();
        Import(physicalExaminationDataList);
    }

    private void OpenWebsite()
    {
        Browser.Get(WebConfig.TargetUrl);
    }

    private void Login()
    {
        Browser.SendKeys(By.XPath("//div[@id='usertext']//div//input"), ApplicationContext.Username);
        Browser.SendKeys(By.Id("pwd"), ApplicationContext.Password);
        Browser.Click(By.XPath("//div[@id='select-role']//div//input"));
        Thread.Sleep(500);
        Browser.Click(By.XPath("//li[text()='责任医生']//parent::ul"));
        Thread.Sleep(500);
        Browser.Click(By.XPath("//li[text()='责任医生']//parent::ul"));
        Browser.Click(By.Id("logon"));
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