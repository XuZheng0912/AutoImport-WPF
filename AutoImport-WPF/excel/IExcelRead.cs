using NPOI.SS.UserModel;

namespace AutoImport_WPF.excel;

public interface IExcelRead
{
    IWorkbook Read(string fileName);
}