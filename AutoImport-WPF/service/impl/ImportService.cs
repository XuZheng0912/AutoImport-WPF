using AutoImport_WPF.import;
using AutoImport_WPF.import.impl;

namespace AutoImport_WPF.service.impl;

public class ImportService : IImportService
{
    public void ImportHealthForm(string fileName)
    {
        var importer = new HealthFormImporter();
        importer.Import(fileName);
    }

    public void ImportContract(string fileName)
    {
        var importer = new ContractImporter();
        importer.Import(fileName);
    }

    public void SaveContract(string fileName)
    {   
        var importer = new ContractSaver();
        importer.Import(fileName);
    }
}