using NPOI.SS.UserModel;

namespace AutoImport_WPF.domain;

public class PhysicalExaminationData(IRow row)
{
    public string Id => GetCell(6);


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
        return string.IsNullOrWhiteSpace(GetCell(col));
    }

    private string GetCell(int col)
    {
        return row.GetCell(col).StringCellValue;
    }
}