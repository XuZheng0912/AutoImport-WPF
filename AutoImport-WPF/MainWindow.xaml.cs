using System.Windows;
using AutoImport_WPF.domain;
using AutoImport_WPF.log;
using AutoImport_WPF.service;
using Microsoft.Win32;

namespace AutoImport_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly PoiExcelReadService _excelReadService;

    private static ILogger Logger => LogConfig.Logger;

    public MainWindow()
    {
        InitializeComponent();
        var logWindow = new LogWindow();
        logWindow.Show();
        LogConfig.Logger = new CommonLogger(content =>
        {
            if (!logWindow.IsVisible)
            {
                logWindow.Show();
            }

            logWindow.AddListItem(content);
        });
        _excelReadService = new PoiExcelReadService();
    }

    private void excelFileSelectButtonOnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls"
        };

        if (openFileDialog.ShowDialog() != true) return;
        var fileName = openFileDialog.FileName;
        Logger.Debug($"文件路径：{fileName}");
        ReadFromExcel(fileName);
    }

    private List<PhysicalExaminationData> ReadFromExcel(string filePath)
    {
        return _excelReadService.Read(filePath);
    }
}