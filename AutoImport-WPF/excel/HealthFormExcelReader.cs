using AutoImport_WPF.domain;

namespace AutoImport_WPF.excel;

public class HealthFormExcelReader : ExcelDataReader<HealthFormData>
{
    protected override HealthFormData Build(ExcelRow row)
    {
        return new HealthFormData(row);
    }
}