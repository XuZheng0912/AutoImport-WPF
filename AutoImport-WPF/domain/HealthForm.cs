namespace AutoImport_WPF.domain;

public class HealthForm(ExcelRow row) : IHealthForm
{
    public string CheckDate => ToY_M_D(row[0]);

    public string Name => row[1];

    public string Id => row[CellIndex(7)];

    public bool IsElder => !string.IsNullOrWhiteSpace(row[CellIndex(8)]);

    public bool IsHypertension => !string.IsNullOrWhiteSpace(row[CellIndex(9)]);

    public bool IsDiabetes => !string.IsNullOrWhiteSpace(row[CellIndex(10)]);

    public string Temperature => RandomTemperature();

    public string HeartRate => row[CellIndex(41)];

    public string BreathRate => RandomBreathRate();

    public string RightConstriction => row[CellIndex(44)];

    public string RightDiastolic => row[CellIndex(45)];

    public string LeftConstriction => row[CellIndex(42)];

    public string LeftDiastolic => row[CellIndex(43)];

    public string Height => row[CellIndex(81)];

    public string Weight => row[CellIndex(82)];

    public string Waistline => row[CellIndex(83)];

    private string _eyeSight = "";

    public string LeftEye => EyeSight();

    public string RightEye => EyeSight();

    public string Hemoglobin => row[CellIndex(60)];

    public string WhiteBloodCell => row[CellIndex(62)];

    public string Platelet => row[CellIndex(63)];

    public string FastingBloodGlucose => row[CellIndex(52)];

    public bool IsEcgNormal => row[CellIndex(79)].Trim().Equals("正常");

    public string EcgText => row[CellIndex(80)];

    public string SerumAlanineAminotransferase => row[CellIndex(55)];

    public string SerumGlutamicOxalaceticTransaminase => row[CellIndex(56)];

    public string TotalBilirubin => row[CellIndex(58)];

    public string SerumCreatinine => row[CellIndex(73)];

    public string BloodUreaNitrogen => row[CellIndex(74)];

    public string TotalCholesterol => row[CellIndex(69)];

    public string Triglyceride => row[CellIndex(70)];

    public string SerumLowDensityLipoproteinCholesterol => row[CellIndex(71)];

    public string SerumHighDensityLipoproteinCholesterol => row[CellIndex(72)];

    public bool IsChestXrayNormal => row[CellIndex(75)].Trim().Equals("正常");

    public string Xtext => row[CellIndex(76)];

    public bool IsBUltrasonicNormal => row[CellIndex(77)].Trim().Equals("正常");

    public string BText => row[CellIndex(78)];

    public string OtherSystemDisease
    {
        get
        {
            if (IsDiabetes && IsHypertension)
            {
                return "高血压，糖尿病";
            }

            return IsDiabetes ? "糖尿病" : "高血压";
        }
    }

    public string Medicine1 => row[CellIndex(230)];
    public string MedicineUse1 => row[CellIndex(231)];
    public string MedicineEachDose1 => row[CellIndex(232)];
    public string MedicineUseDate1 => row[CellIndex(233)];
    public string MedicineYield1 => row[CellIndex(234)];
    public string Medicine2 => row[CellIndex(235)];
    public string MedicineUse2 => row[CellIndex(236)];
    public string MedicineEachDose2 => row[CellIndex(237)];
    public string MedicineUseDate2 => row[CellIndex(238)];
    public string MedicineYield2 => row[CellIndex(239)];
    public string Medicine3 => row[CellIndex(240)];
    public string MedicineUse3 => row[CellIndex(241)];
    public string MedicineEachDose3 => row[CellIndex(242)];
    public string MedicineUseDate3 => row[CellIndex(243)];
    public string MedicineYield3 => row[CellIndex(244)];
    public string Medicine4 => row[CellIndex(245)];
    public string MedicineUse4 => row[CellIndex(246)];
    public string MedicineEachDose4 => row[CellIndex(247)];
    public string MedicineUseDate4 => row[CellIndex(248)];
    public string MedicineYield4 => row[CellIndex(249)];

    public bool HasAbnormal => row[CellIndex(105)].Trim().Equals("有");

    public string Abnormality1 => row[CellIndex(113)];
    public string Abnormality2 => row[CellIndex(125)];
    public string Abnormality3 => row[CellIndex(169)];
    public string Abnormality4 => row[CellIndex(170)];

    public bool IsPutIntoAdministration => !string.IsNullOrWhiteSpace(row[CellIndex(217)]);
    public bool IsSuggestedReview => !string.IsNullOrWhiteSpace(row[CellIndex(218)]);
    public bool IsSuggestReferral => !string.IsNullOrWhiteSpace(row[CellIndex(219)]);
    public bool IsNeedQuitSmoking => !string.IsNullOrWhiteSpace(row[CellIndex(220)]);
    public bool IsNeedHealthDrinking => !string.IsNullOrWhiteSpace(row[CellIndex(221)]);
    public bool IsNeedDiet => !string.IsNullOrWhiteSpace(row[CellIndex(222)]);
    public bool IsNeedExercise => !string.IsNullOrWhiteSpace(row[CellIndex(223)]);
    public bool IsNeedLostWeight => !string.IsNullOrWhiteSpace(row[CellIndex(224)]);
    public string TargetWeight => row[CellIndex(225)];

    public bool IsSuggestVaccination => !string.IsNullOrWhiteSpace(row[CellIndex(226)]);
    public string Vaccination => row[CellIndex(227)];

    public bool HasOther => !string.IsNullOrWhiteSpace(row[CellIndex(228)]);
    public string OtherSuggestion => row[CellIndex(229)];

    private string EyeSight()
    {
        if (string.IsNullOrWhiteSpace(_eyeSight))
        {
            _eyeSight = RandomEyeSight();
        }

        return _eyeSight;
    }

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

    private static string ToY_M_D(string dateStr)
    {
        return DateTime.Parse(dateStr).ToString("yyyy-MM-dd");
    }

    private static int CellIndex(int cellNum)
    {
        return cellNum - 2;
    }
}