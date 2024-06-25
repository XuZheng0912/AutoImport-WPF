using System.Globalization;
using NPOI.SS.UserModel;

namespace AutoImport_WPF.domain;

public class HealthFormData(ExcelRow row) : ExcelRowData(row)
{
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

    public bool IsElder()
    {
        return IsCellNullOrWhiteSpace(7);
    }

    public bool IsHypertension()
    {
        return IsCellNullOrWhiteSpace(8);
    }

    public bool IsDiabetes()
    {
        return IsCellNullOrWhiteSpace(9);
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