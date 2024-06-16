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
                Logger.Info($"{contractData.Name}-{contractData.Id}导入异常");
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
                Logger.Info("正在登入网站");
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
        List<string> limitImgPossibleXpathList =
        [
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[1]/div/img",
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[1]/div/img"
        ];
        Browser.ClickByPossibleXpathList(limitImgPossibleXpathList);
        List<string> idLimitPossibleXpathList =
        [
            "/html/body/div[12]/div/div[6]",
            "/html/body/div[17]/div/div[6]",
            "/html/body/div[8]/div/div[7]"
        ];
        Browser.ClickByPossibleXpathList(idLimitPossibleXpathList);
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
        ReadyForImport();
        const string idCardName = "idCard";
        Thread.Sleep(500);
        Browser.ClearByName(idCardName);
        Browser.SendKeysByName(idCardName, contractData.Id);
        Browser.ClickByPossibleXpathList
        ([
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[5]/table/tbody/tr[2]/td[2]/em/button",
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[5]/table/tbody/tr[2]/td[2]/em/button",
            "/html/body/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[1]/div/table/tbody/tr/td[1]/table/tbody/tr/td[5]/table/tbody/tr[2]/td[2]/em/button"
        ]);
        Thread.Sleep(500);
        List<string> possibleSearchResultXpathList =
        [
            "/html/body/div[2]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[2]/div/div[1]/div[2]/div",
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[2]/div/div[1]/div[2]/div"
        ];
        Browser.DoubleClickByPossibleXpath(possibleSearchResultXpathList);
        Browser.ClickByXpath("//button[text()='保存(F1)']");
        Thread.Sleep(1000);
        Browser.ClickByXpath("//button[text()='确定']");
        Thread.Sleep(1000);
        Browser.ClickByXpath(
            "/html/body/div[10]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div[2]/div/div/div/div/ul/div/li[2]/div/img[1]");
        Browser.ClickByXpath(
            "/html/body/div[10]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div[2]/div/div/div/div/ul/div/li[2]/ul/li[1]/div");
        Thread.Sleep(1500);
        Browser.ClickByText("签约(F1)");
        Thread.Sleep(1500);
        Browser.ClickByXpath(
            "/html/body/div[10]/div[2]/div[1]/div/div/div/div/div/div/div[3]/div[2]/div/div[4]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div/div[1]/div/div/div/div[2]/div[1]/div/div/form/table/tbody/tr[2]/td[1]/div/div[1]/div/img");
        SelectType(contractData.Type);
        Browser.ClickByXpath(
            "/html/body/div[10]/div[2]/div[1]/div/div/div/div/div/div/div[3]/div[2]/div/div[4]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div/div[1]/div/div/div/div[2]/div[1]/div/div/form/table/tbody/tr[2]/td[1]/div/div[1]/div/img");
        List<string> checkBoxXpathList =
        [
            "/html/body/div[12]/div[2]/div[1]/div/div/div/div/div/div/div[3]/div[2]/div/div[4]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div[2]/div/div[1]/div[1]/div[1]/div/table/thead/tr/td[1]/div/div",
            "/html/body/div[11]/div[2]/div[1]/div/div/div/div/div/div/div[3]/div[2]/div/div[4]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div[2]/div/div[1]/div[1]/div[1]/div/table/thead/tr/td[1]/div/div",
            "/html/body/div[10]/div[2]/div[1]/div/div/div/div/div/div/div[3]/div[2]/div/div[4]/div[2]/div[1]/div/div/div/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div[2]/div[1]/div/div/div[2]/div/div[1]/div[1]/div[1]/div/table/thead/tr/td[1]/div/div"
        ];
        Browser.ClickByPossibleXpathList(checkBoxXpathList);
        Thread.Sleep(500);
        Browser.ClickByText("保存(F1)");
        Browser.ClickByText("是");
        Thread.Sleep(500);
        Browser.ClickById("CLOSE");
    }

    private static void SelectType(string type)
    {
        var types = type.Split(",");
        if (string.IsNullOrWhiteSpace(type))
        {
            Browser.ClickByXpath("/html/body/div[29]/div/div[1]/div");
        }
    }

    private static List<Action> SelectTypeActions(string[] types)
    {
        return types.Select<string, Action>(type => type switch
            {
                not null when string.IsNullOrWhiteSpace(type) => () =>
                    Browser.ClickByXpath("/html/body/div[29]/div/div[1]/div"),
                // "老年人" => () => Browser.ClickByXpath("/html/body/div[29]/div/div[2]/div"),
                // "0-6岁儿童" => () => Browser.ClickByXpath("/html/body/div[29]/div/div[3]/div"),
                // "建档立卡" => () => Browser.ClickByXpath("/html/body/div[29]/div/div[16]/div"),
                // "残疾人" => () => Browser.ClickByXpath("/html/body/div[29]/div/div[13]/div"),
                // "高血压" => () => Browser.ClickByXpath("/html/body/div[29]/div/div[5]/div"),
                // "糖尿病" => () => Browser.ClickByXpath("/html/body/div[29]/div/div[6]/div"),
                _ => () => { }
            }
        ).ToList();
    }

    private static List<ContractData> ReadFromExcelFile(string fileName)
    {
        return new ContractExcelReader().Read(fileName);
    }
}