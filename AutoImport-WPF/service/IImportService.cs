using AutoImport_WPF.domain;

namespace AutoImport_WPF.service;

public interface IImportService
{
    void Import(string fileName);

    void Import(List<PhysicalExaminationData> data);
}