using NPOI.SS.UserModel;

namespace AutoImport_WPF.domain;

public class HealthFormData(ExcelRow row) : ExcelRowData(row)
{
    public string Id => base[6];


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
}