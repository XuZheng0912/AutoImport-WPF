using AutoImport_WPF.domain;

namespace AutoImport_WPF.excel;

public class CompleteHealthFormReader : ExcelDataReader<IHealthForm>
{
    private CompleteHealthFormReader()
    {
    }

    public static CompleteHealthFormReader NewInstance()
    {
        return new CompleteHealthFormReader();
    }

    protected override IHealthForm Build(ExcelRow row)
    {
        return new HealthForm(row);
    }
}