namespace AutoImport_WPF.service;

public interface IImportService
{
    void ImportHealthForm(string fileName);

    void ImportContract(string fileName);
}