using AutoImport_WPF.browser;
using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.domain;
using AutoImport_WPF.excel;
using AutoImport_WPF.log;
using OpenQA.Selenium;

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
        ReadyForImport();
        foreach (var healthFormData in dataList)
        {
            try
            {
                Import(healthFormData);
            }
            catch (Exception)
            {
                Logger.Info($"{healthFormData.Name}-{healthFormData.Id}导入异常");
            }
        }
    }

    private static void Import(HealthFormData healthFormData)
    {
        const string idCardName = "idCard";
        Thread.Sleep(500);
        Browser.ClearByName(idCardName);
        Browser.SendKeysByName(idCardName, healthFormData.Id);
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
        Browser.ClickByXpath("//button[text()='新建(F2)']");

        if (healthFormData.IsElder())
        {
            SelectOptionWhenNoSelected("checkWay", "2");
        }

        if (healthFormData.IsHypertension())
        {
            SelectOptionWhenNoSelected("checkWay", "3");
        }

        if (healthFormData.IsDiabetes())
        {
            SelectOptionWhenNoSelected("checkWay", "4");
        }

        SelectOptionWhenNoSelected("symptom", "01");
        SendKeysWhenInputEmpty("temperature", healthFormData.Temperature);
        SendKeysWhenInputEmpty("pulse", healthFormData.PulseRate);
        SendKeysWhenInputEmpty("breathe", healthFormData.BreathRate);
        SendKeysWhenValueNotEmpty("constriction", healthFormData.RightConstriction);
        SendKeysWhenValueNotEmpty("diastolic", healthFormData.RightDiastolic);
        SendKeysWhenValueNotEmpty("constriction_L", healthFormData.LeftConstriction);
        SendKeysWhenValueNotEmpty("diastolic_L", healthFormData.LeftDiastolic);
    }

    private static void SendKeysWhenInputEmpty(string elementName, string value)
    {
        SendKeysWhen(elementName, value, () => string.IsNullOrWhiteSpace(Browser.GetInputValueByName(elementName)));
    }

    private static void SendKeysWhenValueNotEmpty(string elementName, string value)
    {
        SendKeysWhen(elementName, value, () => !string.IsNullOrWhiteSpace(value));
    }

    private static void SendKeysWhen(string elementName, string value, Func<bool> condition)
    {
        if (!condition()) return;
        Browser.SendKeysByName(elementName, value);
    }

    private static void SelectOptionWhenNoSelected(string name, string value)
    {
        if (Browser.IsOptionSelected(name, value))
        {
            return;
        }

        Browser.SelectByName(name, value);
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

    private static void SwitchToPersonalRecordManage()
    {
        var hrIdBy = By.Id("HR");
        Browser.Wait(hrIdBy);
        Browser.Click(hrIdBy);
        var moduleIdBy = By.Id("WL_module_D20");
        Browser.Wait(moduleIdBy);
        Browser.Click(moduleIdBy);
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
            "/html/body/div[8]/div/div[6]",
            "/html/body/div[9]/div/div[6]"
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

    private static List<HealthFormData> ReadFromExcelFile(string fileName)
    {
        return new HealthFormExcelReader().Read(fileName);
    }
}