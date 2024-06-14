using AutoImport_WPF.domain;

namespace AutoImport_WPF.excel;

public class ContractExcelReader : ExcelReader<ContractData>
{
    public override ContractData Build(ExcelRow row)
    {
        return new ContractData(row);
    }
}