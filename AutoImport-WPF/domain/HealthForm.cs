namespace AutoImport_WPF.domain;

public class HealthForm(ExcelRow row) : IHealthForm
{
    public string CheckDate => ToY_M_D(row[CellIndex(1)]);

    public string Name => row[CellIndex(2)];

    public string Id => row[CellIndex(6)];

    public bool IsElder => !string.IsNullOrWhiteSpace(row[CellIndex(7)]);

    private static string ToY_M_D(string dateStr)
    {
        return DateTime.Parse(dateStr).ToString("yyyy-MM-dd");
    }

    private static int CellIndex(int cellNum)
    {
        return cellNum - 1;
    }
}