using System.IO;
using AutoImport_WPF.domain;
using AutoImport_WPF.log;
using NPOI.XSSF.UserModel;

namespace AutoImport_WPF.service.impl;

public class PoiExcelReadService : IExcelReadService
{
    private static ILogger Logger => LogConfig.Logger;

    public List<PhysicalExaminationData> Read(string filePath)
    {
        Logger.Info("开始读取Excel文件数据");
        var workbook = OpenExcel(filePath);
        var sheet = workbook.GetSheetAt(0);
        List<PhysicalExaminationData> list = [];
        for (var rowNum = 1; rowNum < sheet.LastRowNum; rowNum++)
        {
            var row = sheet.GetRow(rowNum);
            list.Add(new PhysicalExaminationData(row));
        }

        Logger.Info("数据读取完成");
        Logger.Debug($"表格数据读取总计{list.Count}行");
        return list;
    }


    private static XSSFWorkbook OpenExcel(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return new XSSFWorkbook(fileStream);
    }
}