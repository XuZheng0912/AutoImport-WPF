using AutoImport_WPF.domain;

namespace AutoImport_WPF.excel;

public class ContractExcelReader : ExcelDataReader<ContractData>
{
    protected override ContractData Build(ExcelRow row)
    {
        return new ContractData(row);
    }
}