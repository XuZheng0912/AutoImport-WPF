﻿namespace AutoImport_WPF.domain;

public class HealthFormData(ExcelRow row) : ExcelRowData(row)
{
    private readonly ExcelRow _row = row;

    public string Date => ToY_M_D(base[0]);

    private static string ToY_M_D(string dateStr)
    {
        return DateTime.Parse(dateStr).ToString("yyyy-MM-dd");
    }

    public string Name => base[1];

    public string Id => base[5];

    public string Temperature => RandomTemperature();

    public string BreathRate => RandomBreathRate();

    public string RightConstriction => base[12];

    public string RightDiastolic => base[13];

    public string LeftConstriction => base[10];

    public string LeftDiastolic => base[11];

    public string FastingBloodGlucose => base[14];

    public string Height => base[15];

    public string Weight => base[16];

    public string Waistline => base[17];

    public string LeftEye => RandomEyeSight();

    public string RightEye => RandomEyeSight();

    public string HeartRate => base[9];

    // private string EcgResult => base[78];

    // public string EcgAbnormal => base[79];

    // private string ChestXrayResult => base[74];
    //
    // public string ChestXrayAbnormal => base[75];
    //
    // private string BUltrasonicResult => base[76];
    //
    // public string BUltrasonicAbnormal => base[77];

    // private string OtherSystemDiseaseResult => base[94];

    public string OtherSystemDisease
    {
        get
        {
            if (IsDiabetes() && IsHypertension())
            {
                return "高血压，糖尿病";
            }

            return IsDiabetes() ? "糖尿病" : "高血压";
        }
    }

    public string Medicine1 => base[55];

    public string MedicineUse1 => base[56];

    public string MedicineEachDose1 => base[57];

    public string MedicineUseDate1 => base[58];

    public string MedicineYield1 => base[59];

    public string Medicine2 => base[60];

    public string MedicineUse2 => base[61];

    public string MedicineEachDose2 => base[62];

    public string MedicineUseDate2 => base[63];

    public string MedicineYield2 => base[64];

    public string Medicine3 => base[65];

    public string MedicineUse3 => base[66];

    public string MedicineEachDose3 => base[67];

    public string MedicineUseDate3 => base[68];

    public string MedicineYield3 => base[69];

    public string Medicine4 => base[70];

    public string MedicineUse4 => base[71];

    public string MedicineEachDose4 => base[72];

    public string MedicineUseDate4 => base[73];

    public string MedicineYield4 => base[74];

    private string AbnormalResult => base[20];

    public string Abnormality1 => base[28];
    // public string Abnormality2 => base[106];
    // public string Abnormality3 => base[107];
    // public string Abnormality4 => base[108];

    private string PutIntoAdministration => base[44];

    private string SuggestedReview => base[45];

    private string SuggestedReferral => base[46];

    private string QuitSmoking => base[47];

    private string HealthDrinking => base[48];

    private string Diet => base[49];

    private string Exercise => base[50];

    private string LostWeight => base[51];

    public double TargetWeight => _row.GetNumberValue(52);

    // private string SuggestedVaccination => base[118];

    // public string Vaccination => base[119];

    private string Other => base[53];

    public string OtherSuggestion => base[54];

    public bool HasOther()
    {
        return !string.IsNullOrWhiteSpace(Other);
    }

    public bool IsNeedLostWeight()
    {
        return !string.IsNullOrWhiteSpace(LostWeight);
    }

    public bool IsNeedExercise()
    {
        return !string.IsNullOrWhiteSpace(Exercise);
    }

    public bool IsNeedDiet()
    {
        return !string.IsNullOrWhiteSpace(Diet);
    }

    public bool IsNeedHealthDrinking()
    {
        return !string.IsNullOrWhiteSpace(HealthDrinking);
    }

    public bool IsNeedQuitSmoking()
    {
        return !string.IsNullOrWhiteSpace(QuitSmoking);
    }

    public bool IsSuggestReferral()
    {
        return !string.IsNullOrWhiteSpace(SuggestedReferral);
    }

    public bool IsSuggestedReview()
    {
        return !string.IsNullOrWhiteSpace(base[21]) || !string.IsNullOrWhiteSpace(base[23]);
    }

    public bool IsPutIntoAdministration()
    {
        return !string.IsNullOrWhiteSpace(PutIntoAdministration);
    }

    public bool HasAbnormal()
    {
        return !string.IsNullOrWhiteSpace(Abnormality1);
    }

    // public bool HasOtherSystemDisease()
    // {
    //     return "有".Equals(OtherSystemDiseaseResult.Trim());
    // }
    //
    // public bool IsBUltrasonicNormal()
    // {
    //     if ("正常".Equals(BUltrasonicResult.Trim()))
    //     {
    //         return true;
    //     }
    //
    //     var trimResult = BUltrasonicAbnormal.Trim();
    //     if ("拒检".Equals(trimResult) || "未检".Equals(trimResult))
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }

    // public bool IsEcgNormal()
    // {
    //     return "正常".Equals(EcgResult.Trim());
    // }
    //
    // public bool IsChestXrayNormal()
    // {
    //     return "正常".Equals(ChestXrayResult.Trim());
    // }

    public bool IsElder()
    {
        return !IsCellNullOrWhiteSpace(6);
    }

    public bool IsHypertension()
    {
        return !IsCellNullOrWhiteSpace(7);
    }

    public bool IsDiabetes()
    {
        return !IsCellNullOrWhiteSpace(8);
    }

    private bool IsCellNullOrWhiteSpace(int col)
    {
        return string.IsNullOrWhiteSpace(base[col]);
    }

    private static string RandomTemperature()
    {
        // 生成36.5到37.2之间的随机浮点数
        const double minTemperature = 36.5;
        const double maxTemperature = 37.2;
        var random = new Random();
        // 生成随机数
        var randomTemperature = random.NextDouble() * (maxTemperature - minTemperature) + minTemperature;

        return randomTemperature.ToString("0.0");
    }

    private static string RandomEyeSight()
    {
        const double minTemperature = 4.5;
        const double maxTemperature = 4.8;
        var random = new Random();
        // 生成随机数
        var randomTemperature = random.NextDouble() * (maxTemperature - minTemperature) + minTemperature;

        return randomTemperature.ToString("0.0");
    }

    // private static string RandomPulseRate()
    // {
    //     // 生成60到100之间的随机整数
    //     const int minNumber = 60;
    //     const int maxNumber = 100;
    //     var random = new Random();
    //     // 生成随机整数
    //     var randomNumber = random.Next(minNumber, maxNumber + 1); // 注意：maxValue 是 exclusive 的，所以需要 +1
    //
    //     // 将随机整数转换为字符串
    //     return randomNumber.ToString();
    // }


    private static string RandomBreathRate()
    {
        // 生成60到100之间的随机整数
        const int minNumber = 16;
        const int maxNumber = 18;
        var random = new Random();
        // 生成随机整数
        var randomNumber = random.Next(minNumber, maxNumber + 1); // 注意：maxValue 是 exclusive 的，所以需要 +1

        // 将随机整数转换为字符串
        return randomNumber.ToString();
    }
}