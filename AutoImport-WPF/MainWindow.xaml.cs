using System.Windows;
using AutoImport_WPF.context;
using AutoImport_WPF.log;
using Microsoft.Win32;

namespace AutoImport_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static ILogger Logger => LogConfig.Logger;

    public MainWindow()
    {
        InitializeComponent();
        var logWindow = new LogWindow();
        logWindow.Show();
        LogConfig.Logger = new CommonLogger(content => logWindow.AddListItem(content));
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
        ApplicationContext.FileName = fileName;
    }

    private void ImportButtonOnClick(object sender, RoutedEventArgs e)
    {
        
    }
}