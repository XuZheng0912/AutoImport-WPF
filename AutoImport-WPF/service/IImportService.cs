namespace AutoImport_WPF.service;

public interface IImportService
{
    void ImportHealthForm(string fileName);

    void CompleteHealthForm(string fileName);

    void ImportContract(string fileName);

    void SaveContract(string fileName);
}