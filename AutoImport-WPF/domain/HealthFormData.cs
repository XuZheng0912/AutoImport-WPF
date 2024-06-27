namespace AutoImport_WPF.domain;

public class HealthFormData(ExcelRow row) : ExcelRowData(row)
{
    private readonly ExcelRow _row = row;
    public string Name => base[2];

    public string Id => base[6];

    public string Temperature => RandomTemperature();

    public string PulseRate => RandomPulseRate();

    public string BreathRate => RandomBreathRate();

    public string RightConstriction => base[41];

    public string RightDiastolic => base[42];

    public string LeftConstriction => base[43];

    public string LeftDiastolic => base[44];

    public string Height => base[80];

    public string Weight => base[81];

    public string Waistline => base[82];

    public string LeftEye => RandomEyeSight();

    public string RightEye => RandomEyeSight();

    public string HeartRate => base[36];

    private string EcgResult => base[78];

    public string EcgAbnormal => base[79];

    private string ChestXrayResult => base[74];

    public string ChestXrayAbnormal => base[75];

    private string BUltrasonicResult => base[76];

    public string BUltrasonicAbnormal => base[77];

    private string OtherSystemDiseaseResult => base[94];

    public string OtherSystemDisease => base[95];

    public string Medicine1 => base[122];

    public string MedicineUse1 => base[123];

    public string MedicineEachDose1 => base[124];

    public string MedicineUseDate1 => base[125];

    public string MedicineYield1 => base[126];

    public string Medicine2 => base[127];

    public string MedicineUse2 => base[128];

    public string MedicineEachDose2 => base[129];

    public string MedicineUseDate2 => base[130];

    public string MedicineYield2 => base[131];

    public string Medicine3 => base[132];

    public string MedicineUse3 => base[133];

    public string MedicineEachDose3 => base[134];

    public string MedicineUseDate3 => base[135];

    public string MedicineYield3 => base[136];

    public string Medicine4 => base[137];

    public string MedicineUse4 => base[138];

    public string MedicineEachDose4 => base[139];

    public string MedicineUseDate4 => base[140];

    public string MedicineYield4 => base[141];

    private string AbnormalResult => base[104];

    public string Abnormality1 => base[105];
    public string Abnormality2 => base[106];
    public string Abnormality3 => base[107];
    public string Abnormality4 => base[108];

    private string PutIntoAdministration => base[109];

    private string SuggestedReview => base[110];

    private string SuggestedReferral => base[111];

    private string QuitSmoking => base[112];

    private string HealthDrinking => base[113];

    private string Diet => base[114];

    private string Exercise => base[115];

    private string LostWeight => base[116];

    public double TargetWeight => _row.GetNumberValue(117);

    private string SuggestedVaccination => base[118];

    public string Vaccination => base[119];

    private string Other => base[120];

    public string OtherSuggestion => base[121];

    public bool HasOther()
    {
        return !string.IsNullOrWhiteSpace(Other);
    }

    public bool IsSuggestedVaccination()
    {
        return !string.IsNullOrWhiteSpace(SuggestedVaccination);
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
        return !string.IsNullOrWhiteSpace(SuggestedReview);
    }

    public bool IsPutIntoAdministration()
    {
        return !string.IsNullOrWhiteSpace(PutIntoAdministration);
    }

    public bool HasAbnormal()
    {
        return "有".Equals(AbnormalResult.Trim());
    }

    public bool HasOtherSystemDisease()
    {
        return "有".Equals(OtherSystemDiseaseResult.Trim());
    }

    public bool IsBUltrasonicNormal()
    {
        if ("正常".Equals(BUltrasonicResult.Trim()))
        {
            return true;
        }

        var trimResult = BUltrasonicAbnormal.Trim();
        if ("拒检".Equals(trimResult) || "未检".Equals(trimResult))
        {
            return true;
        }

        return false;
    }

    public bool IsEcgNormal()
    {
        return "正常".Equals(EcgResult.Trim());
    }

    public bool IsChestXrayNormal()
    {
        return "正常".Equals(ChestXrayResult.Trim());
    }

    public bool IsElder()
    {
        return !IsCellNullOrWhiteSpace(7);
    }

    public bool IsHypertension()
    {
        return !IsCellNullOrWhiteSpace(8);
    }

    public bool IsDiabetes()
    {
        return !IsCellNullOrWhiteSpace(9);
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

    private static string RandomPulseRate()
    {
        // 生成60到100之间的随机整数
        const int minNumber = 60;
        const int maxNumber = 100;
        var random = new Random();
        // 生成随机整数
        var randomNumber = random.Next(minNumber, maxNumber + 1); // 注意：maxValue 是 exclusive 的，所以需要 +1

        // 将随机整数转换为字符串
        return randomNumber.ToString();
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
}