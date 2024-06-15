namespace AutoImport_WPF.domain;

public class ContractData(ExcelRow row)
{
    public string Name => row[1];

    public string Id => row[2];

    public string Type => row[3];
}