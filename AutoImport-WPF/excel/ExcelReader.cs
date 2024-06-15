using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AutoImport_WPF.excel;

public class ExcelReader : IExcelRead
{
    public IWorkbook Read(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return Path.GetExtension(filePath) switch
        {
            "xls" => new HSSFWorkbook(fileStream),
            "xlsx" => new XSSFWorkbook(fileStream),
            _ => throw new FormatException("文件格式错误，只能读取xls或xlsx文件")
        };
    }
}