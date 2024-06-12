using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.domain;
using AutoImport_WPF.log;
using OpenQA.Selenium;

namespace AutoImport_WPF.service.impl;

public class ImportService : IImportService
{
    private readonly IExcelReadService _excelReadService = new PoiExcelReadService();

    private IBrowser Browser { get; } = new ChromeBrowser();

    private static ILogger Logger => LogConfig.Logger;

    public void Import(string fileName)
    {
        var physicalExaminationDataList = ReadFromExcel(fileName);
        OpenWebsite();
        Login();
        SwitchToPhysicalExam();
        SetQueryLimitId();
        Import(physicalExaminationDataList);
    }

    private void SetQueryLimitId()
    {
        List<string> limitImgPossibleXpath =
        [
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[1]/div/input",
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[1]/div/input"
        ];
        Click(limitImgPossibleXpath);
        List<string> idLimitPossibleXpath =
        [
            "/html/body/div[12]/div/div[6]",
            "/html/body/div[8]/div/div[6]",
            "/html/body/div[17]/div/div[6]"
        ];
        Click(idLimitPossibleXpath);
    }

    private void Click(List<string> possibleXpath)
    {
        foreach (var xpath in possibleXpath)
        {
            try
            {
                Browser.Click(By.XPath(xpath));
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    private void SwitchToPhysicalExam()
    {
        var hrIdBy = By.Id("HR");
        Browser.Wait(hrIdBy);
        Browser.Click(hrIdBy);
        var moduleIdBy = By.Id("WL_module_D20");
        Browser.Wait(moduleIdBy);
        Browser.Click(moduleIdBy);
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

    private void Import(PhysicalExaminationData data)
    {
        var idLimitInput = By.XPath("");
        Browser.Clear(idLimitInput);
        Browser.SendKeys(idLimitInput, data.Id);
    }

    private List<PhysicalExaminationData> ReadFromExcel(string fileName)
    {
        return _excelReadService.Read(fileName);
    }
}