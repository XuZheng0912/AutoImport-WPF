using System.IO;
using AutoImport_WPF.domain;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AutoImport_WPF.service;

public class PoiExcelReadService(Action<string> log) : IExcelReadService
{
    private readonly Action<string> _log = log;

    public List<PhysicalExaminationData> Read(string filePath)
    {
        var workbook = OpenExcel(filePath);
        var sheet = workbook.GetSheetAt(0);
        List<PhysicalExaminationData> list = [];
        for (var rowNum = 1; rowNum < sheet.LastRowNum; rowNum++)
        {
            var row = sheet.GetRow(rowNum);
            list.Add(new PhysicalExaminationData(row));
        }

        return list;
    }


    private static XSSFWorkbook OpenExcel(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return new XSSFWorkbook(fileStream);
    }
}