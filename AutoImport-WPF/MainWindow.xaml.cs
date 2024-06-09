using System.Windows;
using AutoImport_WPF.domain;
using AutoImport_WPF.service;
using Microsoft.Win32;

namespace AutoImport_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly LogWindow _logWindow;

    private readonly IExcelReadService _excelReadService;

    public MainWindow()
    {
        InitializeComponent();
        _logWindow = new LogWindow();
        _logWindow.Show();
        _excelReadService = new PoiExcelReadService(Debug);
    }

    private void excelFileSelectButtonOnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls"
        };

        if (openFileDialog.ShowDialog() != true) return;
        var fileName = openFileDialog.FileName;
        Debug(fileName);
        Debug(ReadFromExcel(fileName).Count.ToString());
    }

    private List<PhysicalExaminationData> ReadFromExcel(string filePath)
    {
        return _excelReadService.Read(filePath);
    }

    private void Debug(string content)
    {
        _logWindow.Debug(content);
    }
}