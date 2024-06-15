namespace AutoImport_WPF.excel;

public static class ExcelReaderProvider
{
    public static IExcelRead GetReader()
    {
        return new ExcelReader();
    }
}