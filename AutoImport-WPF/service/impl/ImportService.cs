using AutoImport_WPF.import.impl;
using AutoImport_WPF.log;

namespace AutoImport_WPF.service.impl;

public class ImportService : IImportService
{
    public void ImportHealthForm(string fileName)
    {
        throw new NotImplementedException();
    }

    public void ImportContract(string fileName)
    {
        var importer = new ContractImporter();
        importer.Import(fileName);
    }
}