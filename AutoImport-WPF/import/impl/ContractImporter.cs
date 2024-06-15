using AutoImport_WPF.browser;
using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.domain;
using AutoImport_WPF.excel;
using AutoImport_WPF.log;
using OpenQA.Selenium;

namespace AutoImport_WPF.import.impl;

public class ContractImporter : IFileImport, IListDataImport<ContractData>
{
    private static ILogger Logger => LogConfig.Logger;

    private static IBrowser Browser => BrowserConfig.Browser;

    public void Import(string filename)
    {
        var contractDataList = ReadFromExcelFile(filename);
        Import(contractDataList);
    }

    public void Import(List<ContractData> dataList)
    {
        ReadyForImport();
        foreach (var contractData in dataList)
        {
            try
            {
                Import(contractData);
            }
            catch (Exception)
            {
                Logger.Debug($"{contractData.Name}-{contractData.Id}导入异常");
                Browser.Close();
                ReadyForImport();
            }
        }
    }

    private static void ReadyForImport()
    {
        var success = false;
        do
        {
            try
            {
                PrepareForImport();
                success = true;
            }
            catch (Exception)
            {
                Logger.Debug("准备导入失败，正在重新登入网站");
            }
        } while (!success);
    }

    private static void PrepareForImport()
    {
        OpenWebsite();
        Login();
        SwitchToPersonalRecordManage();
        SetQueryLimitId();
    }

    private static void SetQueryLimitId()
    {
        List<string> limitImgPossibleXpath =
        [
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[1]/div/input",
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[1]/div/input"
        ];
        Click(limitImgPossibleXpath);
        // List<string> idLimitPossibleXpath =
        // [
        //     "/html/body/div[12]/div/div[6]",
        //     "/html/body/div[8]/div/div[6]",
        //     "/html/body/div[17]/div/div[6]",
        //     "/html/body/div[8]/div/div[7]"
        // ];
        Browser.Click(By.XPath("//div[text()='身份证号码']"));
        // Click(idLimitPossibleXpath);
    }

    private static void Click(List<string> possibleXpath)
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

    private static void OpenWebsite()
    {
        Browser.Get(WebConfig.TargetUrl);
    }

    private static void Login()
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

    private static void SwitchToPersonalRecordManage()
    {
        var hrIdBy = By.Id("HR");
        Browser.Wait(hrIdBy);
        Browser.Click(hrIdBy);
        var moduleIdBy = By.Id("WL_module_B08");
        Browser.Wait(moduleIdBy);
        Browser.Click(moduleIdBy);
    }

    private static void Import(ContractData contractData)
    {
    }

    private static List<ContractData> ReadFromExcelFile(string fileName)
    {
        return new ContractExcelReader().Read(fileName);
    }
}