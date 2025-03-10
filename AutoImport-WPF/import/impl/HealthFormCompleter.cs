﻿using AutoImport_WPF.browser;
using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.domain;
using AutoImport_WPF.excel;
using AutoImport_WPF.log;
using OpenQA.Selenium;

namespace AutoImport_WPF.import.impl;

public class HealthFormCompleter : IFileImport, IListDataImport<IHealthForm>
{
    private static ILogger Logger => LogConfig.Logger;

    private static IBrowser Browser => BrowserConfig.Browser;

    public void Import(string filename)
    {
        var excelDataList = ReadFromExcelFile(filename);
        const int start = 2;
        var healthFormDataList = excelDataList.GetRange(start, excelDataList.Count - start);
        Import(healthFormDataList);
    }

    public void Import(List<IHealthForm> dataList)
    {
        ReadyForImport();
        foreach (var healthFormData in dataList)
        {
            try
            {
                Import(healthFormData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Logger.Info($"{healthFormData.Name}-{healthFormData.Id}导入异常");
                ReadyForImport();
            }
        }

        // foreach (var healthFormData in dataList)
        // {
        //     Import(healthFormData);
        // }
    }

    private static void Import(IHealthForm healthFormData)
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
        Browser.DoubleClickFirstByXpath(
            "/html/body/div[1]/div/div/div[2]/table/tbody/tr[1]/td[3]/div/div[2]/div/div/div[2]/div[1]/div/div/div/div[2]/div/div/div/div/div[1]/div[1]/div/div/div/div[2]/div[1]/div/div/div[2]/div/div[1]/div[2]/div/div");
        Thread.Sleep(2000);

        var forms = Browser.WebDriver().FindElements(By.XPath("//div[@class='x-grid3-cell-inner x-grid3-col-0']"));

        var targetFrom = forms.FirstOrDefault(form => form.Text.Equals(healthFormData.CheckDate));
        if (targetFrom == null)
        {
            Logger.Info($"{healthFormData.Id}-{healthFormData.Name}: 未找到{healthFormData.CheckDate}日期的体检表");
            return;
        }

        targetFrom.Click();

        Thread.Sleep(1500);
        if (healthFormData.IsElder)
        {
            SelectOptionWhenNoSelected("checkWay", "2");
        }

        if (healthFormData.IsHypertension)
        {
            SelectOptionWhenNoSelected("checkWay", "3");
        }

        if (healthFormData.IsDiabetes)
        {
            SelectOptionWhenNoSelected("checkWay", "4");
        }

        SelectOptionWhenNoSelected("symptom", "01");
        SendKeysWhenInputEmpty("temperature", healthFormData.Temperature);
        SendKeysWhenValueNotEmpty("pulse", healthFormData.HeartRate);
        SendKeysWhenInputEmpty("breathe", healthFormData.BreathRate);
        SendKeysWhenValueNotEmpty("constriction", healthFormData.RightConstriction);
        SendKeysWhenValueNotEmpty("diastolic", healthFormData.RightDiastolic);
        SendKeysWhenValueNotEmpty("constriction_L", healthFormData.LeftConstriction);
        SendKeysWhenValueNotEmpty("diastolic_L", healthFormData.LeftDiastolic);
        SendKeysWhenValueNotEmpty("height", healthFormData.Height);
        SendKeysWhenValueNotEmpty("weight", healthFormData.Weight);
        SendKeysWhenValueNotEmpty("waistline", healthFormData.Waistline);

        if (healthFormData.IsElder)
        {
            SelectOptionWhenAllNoSelected("healthStatus", "1");
            SelectOptionWhenAllNoSelected("selfCare", "1");
        }


        SelectOptionWhenAllNoSelected("physicalExerciseFrequency", "4");
        SelectOptionWhenAllNoSelected("dietaryHabit", "1");
        SelectOptionWhenAllNoSelected("wehtherSmoke", "1");
        SelectOptionWhenAllNoSelected("drinkingFrequency", "1");
        SelectOptionWhenAllNoSelected("occupational", "1");
        SelectOptionWhenAllNoSelected("lip", "1");
        SelectOptionWhenAllNoSelected("denture", "1");
        SelectOptionWhenAllNoSelected("pharyngeal", "1");

        SendKeysWhenInputEmpty("leftEye", healthFormData.LeftEye);
        SendKeysWhenInputEmpty("rightEye", healthFormData.RightEye);
        SelectOptionWhenAllNoSelected("hearing", "1");
        SelectOptionWhenAllNoSelected("motion", "1");
        SelectOptionWhenAllNoSelected("skin", "1");
        SelectOptionWhenAllNoSelected("sclera", "1");
        SelectOptionWhenAllNoSelected("lymphnodes", "1");
        SelectOptionWhenAllNoSelected("barrelChest", "1");
        SelectOptionWhenAllNoSelected("breathSound", "1");
        SelectOptionWhenAllNoSelected("rales", "1");

        SendKeysWhenValueNotEmpty("heartRate", healthFormData.HeartRate);
        SelectOptionWhenAllNoSelected("rhythm", "1");
        SelectOptionWhenAllNoSelected("heartMurmur", "1");

        Browser.ScrollTo("abdominAltend");

        SelectOptionWhenAllNoSelected("abdominAltend", "1");
        SelectOptionWhenAllNoSelected("adbominAlmass", "1");
        SelectOptionWhenAllNoSelected("liverBig", "1");
        SelectOptionWhenAllNoSelected("splenomegaly", "1");
        SelectOptionWhenAllNoSelected("dullness", "1");
        SelectOptionWhenAllNoSelected("edema", "1");

        if (healthFormData.IsDiabetes)
        {
            SelectOptionWhenNoSelected("footPulse", "2");
        }

        SendKeysWhenValueNotEmpty("hgb", healthFormData.Hemoglobin);
        SendKeysWhenValueNotEmpty("wbc", healthFormData.WhiteBloodCell);
        SendKeysWhenValueNotEmpty("platelet", healthFormData.Platelet);
        // SendKeysWhenInputEmpty("proteinuria", "(-)");
        // SendKeysWhenInputEmpty("glu", "(-)");
        // SendKeysWhenInputEmpty("dka", "(-)");
        // SendKeysWhenInputEmpty("oc", "(-)");
        // SendKeysWhenInputEmpty("leu", "(-)");

        SendKeysWhenInputEmpty("fbs", healthFormData.FastingBloodGlucose);

        if (healthFormData.IsEcgNormal)
        {
            SelectOptionWhenNoSelected("ecg", "1");
        }
        else
        {
            SelectOptionWhenNoSelected("ecg", "2");
            // SendKeysWhenValueNotEmpty("ecgText", healthFormData.EcgText);
        }

        SendKeysWhenValueNotEmpty("alt", healthFormData.SerumAlanineAminotransferase);
        SendKeysWhenValueNotEmpty("ast", healthFormData.SerumGlutamicOxalaceticTransaminase);
        SendKeysWhenValueNotEmpty("tbil", healthFormData.TotalBilirubin);
        SendKeysWhenValueNotEmpty("cr", healthFormData.SerumCreatinine);
        SendKeysWhenValueNotEmpty("bun", healthFormData.BloodUreaNitrogen);
        SendKeysWhenValueNotEmpty("tc", healthFormData.TotalCholesterol);
        SendKeysWhenValueNotEmpty("tg", healthFormData.Triglyceride);
        SendKeysWhenValueNotEmpty("ldl", healthFormData.SerumLowDensityLipoproteinCholesterol);
        SendKeysWhenValueNotEmpty("hdl", healthFormData.SerumHighDensityLipoproteinCholesterol);


        Action chestAction = healthFormData.ChestXray switch
        {
            "正常" => () => SelectOptionWhenNoSelected("x", "1"),
            "异常" => () => SelectOptionWhenNoSelected("x", "2"),
            _ => () => { }
        };
        chestAction();

        Action bAction = healthFormData.BUltrasonic switch
        {
            "正常" => () => SelectOptionWhenNoSelected("b", "1"),
            "异常" => () => SelectOptionWhenNoSelected("b", "2"),
            _ => () => { }
        };
        bAction();

        SelectOptionWhenAllNoSelected("cerebrovascularDiseases", "1");
        SelectOptionWhenAllNoSelected("kidneyDiseases", "1");
        SelectOptionWhenAllNoSelected("heartDisease", "1");
        SelectOptionWhenAllNoSelected("VascularDisease", "1");
        SelectOptionWhenAllNoSelected("eyeDiseases", "1");
        SelectOptionWhenAllNoSelected("neurologicalDiseases", "1");

        if (healthFormData.IsDiabetes || healthFormData.IsHypertension)
        {
            SelectOptionWhenNoSelected("otherDiseasesone", "2");
            SendKeysWhenValueNotEmpty("otherDiseasesoneDesc", healthFormData.OtherSystemDisease);
        }
        else
        {
            SelectOptionWhenNoSelected("otherDiseasesone", "1");
        }

        Browser.ScrollTo("inhospitalFlag");
        Thread.Sleep(500);
        SelectOptionWhenAllNoSelected("inhospitalFlag", "n");
        SelectOptionWhenAllNoSelected("infamilybedFlag", "n");
        var medicineFlag = healthFormData.MedicineUse1 switch
        {
            { } s when string.IsNullOrWhiteSpace(s) => "n",
            _ => "y"
        };
        SelectOptionWhenNoSelected("medicineFlag", medicineFlag);

        var medicineElementSuffix = GetMedicineElementSuffix();

        SendKeysWhenValueNotEmpty($"medicine_1_{medicineElementSuffix}", healthFormData.Medicine1);
        SendKeysWhenValueNotEmpty("use_1", healthFormData.MedicineUse1);
        SendKeysWhenValueNotEmpty("eachDose_1", healthFormData.MedicineEachDose1);
        SendKeysWhenValueNotEmpty("useDate_1", healthFormData.MedicineUseDate1);
        var yieldOption1 = OptionOfMedicineYield(healthFormData.MedicineYield1);
        if (!string.IsNullOrWhiteSpace(yieldOption1))
        {
            SelectOptionWhenNoSelected("medicineYield1", yieldOption1);
        }

        SendKeysWhenValueNotEmpty($"medicine2_{medicineElementSuffix}", healthFormData.Medicine2);
        SendKeysWhenValueNotEmpty("use_2", healthFormData.MedicineUse2);
        SendKeysWhenValueNotEmpty("eachDose_2", healthFormData.MedicineEachDose2);
        SendKeysWhenValueNotEmpty("useDate_2", healthFormData.MedicineUseDate2);
        var yieldOption2 = OptionOfMedicineYield(healthFormData.MedicineYield2);
        if (!string.IsNullOrWhiteSpace(yieldOption2))
        {
            SelectOptionWhenNoSelected("medicineYield2", yieldOption2);
        }

        SendKeysWhenValueNotEmpty($"medicine_3_{medicineElementSuffix}", healthFormData.Medicine3);
        SendKeysWhenValueNotEmpty("use_3", healthFormData.MedicineUse3);
        SendKeysWhenValueNotEmpty("eachDose_3", healthFormData.MedicineEachDose3);
        SendKeysWhenValueNotEmpty("useDate_3", healthFormData.MedicineUseDate3);
        var yieldOption3 = OptionOfMedicineYield(healthFormData.MedicineYield3);
        if (!string.IsNullOrWhiteSpace(yieldOption3))
        {
            SelectOptionWhenNoSelected("medicineYield3", yieldOption3);
        }

        SendKeysWhenValueNotEmpty($"medicine_4_{medicineElementSuffix}", healthFormData.Medicine4);
        SendKeysWhenValueNotEmpty("use_4", healthFormData.MedicineUse4);
        SendKeysWhenValueNotEmpty("eachDose_4", healthFormData.MedicineEachDose4);
        SendKeysWhenValueNotEmpty("useDate_4", healthFormData.MedicineUseDate4);
        var yieldOption4 = OptionOfMedicineYield(healthFormData.MedicineYield4);
        if (!string.IsNullOrWhiteSpace(yieldOption4))
        {
            SelectOptionWhenNoSelected("medicineYield4", yieldOption4);
        }

        SelectOptionWhenNoSelected("nonimmuneFlag", "n");

        if (healthFormData.HasAbnormal)
        {
            SelectOptionWhenNoSelected("abnormality", "2");
            SendKeysWhenValueNotEmpty("abnormality1", healthFormData.Abnormality1);
            SendKeysWhenValueNotEmpty("abnormality2", healthFormData.Abnormality2);
            SendKeysWhenValueNotEmpty("abnormality3", healthFormData.Abnormality3);
            SendKeysWhenValueNotEmpty("abnormality3", healthFormData.Abnormality4);
        }
        else
        {
            SelectOptionWhenNoSelected("abnormality", "1");
        }

        if (healthFormData.IsPutIntoAdministration)
        {
            SelectOptionWhenNoSelected("mana", "1");
        }

        if (healthFormData.IsSuggestedReview)
        {
            SelectOptionWhenNoSelected("mana", "2");
        }

        if (healthFormData.IsSuggestReferral)
        {
            SelectOptionWhenNoSelected("mana", "3");
        }

        if (healthFormData.IsNeedQuitSmoking)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "1");
        }

        if (healthFormData.IsNeedHealthDrinking)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "2");
        }

        if (healthFormData.IsNeedDiet)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "3");
        }

        if (healthFormData.IsNeedExercise)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "4");
        }

        if (healthFormData.IsNeedLostWeight)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "5");
            SendKeysWhenValueNotEmpty("targetWeight", healthFormData.TargetWeight);
        }

        if (healthFormData.IsSuggestVaccination)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "6");
            SendKeysWhenValueNotEmpty("vaccine", healthFormData.Vaccination);
        }

        if (healthFormData.HasOther)
        {
            SelectOptionWhenNoSelected("riskfactorsControl", "7");
            SendKeysWhenValueNotEmpty("pjOther", healthFormData.OtherSuggestion);
        }

        Browser.ClickByXpath("//button[text()='确定(F1)']");
        Thread.Sleep(2000);
        Browser.ClickById("CLOSE");
    }


    private static string GetMedicineElementSuffix()
    {
        const string prefix = "medicine_1_";
        var element = Browser.WebDriver()
            .FindElements(By.XPath($"//*[contains(@name, '{prefix}')]"))
            .First();
        return element.GetAttribute("name")[prefix.Length..];
    }

    private static string OptionOfMedicineYield(string medicineYield)
    {
        var trim = medicineYield.Trim();
        if ("规律".Equals(trim))
        {
            return "1";
        }

        if ("间断".Equals(trim))
        {
            return "2";
        }

        if ("不服药".Equals(trim))
        {
            return "3";
        }

        return "";
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
        Browser.ClearByName(elementName);
        Browser.SendKeysByName(elementName, value);
    }

    private static void SelectOptionWhenNoSelected(string name, string value)
    {
        SelectOptionWhen(name, value, () => !Browser.IsOptionSelected(name, value));
    }

    private static void SelectOptionWhenAllNoSelected(string name, string value)
    {
        SelectOptionWhen(name, value, () => !Browser.IsOptionSelected(name));
    }

    private static void SelectOptionWhen(string name, string value, Func<bool> condition)
    {
        if (!condition() || string.IsNullOrWhiteSpace(value)) return;
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

    private static List<IHealthForm> ReadFromExcelFile(string fileName)
    {
        return CompleteHealthFormReader.NewInstance().Read(fileName);
    }
}