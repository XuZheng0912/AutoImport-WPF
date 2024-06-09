using NPOI.SS.UserModel;

namespace AutoImport_WPF.domain;

public class PhysicalExaminationData(IRow row)
{
    private readonly IRow _row = row;

    public readonly string Id = row.GetCell(6).StringCellValue;
    
    
}