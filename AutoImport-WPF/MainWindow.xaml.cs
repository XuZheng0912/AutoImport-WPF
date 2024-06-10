using System.IO;
using System.Windows;
using System.Windows.Controls;
using AutoImport_WPF.context;
using AutoImport_WPF.log;
using AutoImport_WPF.service.impl;
using Microsoft.Win32;
using Environment = AutoImport_WPF.config.Environment;

namespace AutoImport_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private static ILogger Logger => LogConfig.Logger;

    public MainWindow()
    {
        InitializeComponent();
        var logWindow = new LogWindow();
        logWindow.Show();
        LogConfig.Logger = new CommonLogger(content => logWindow.AddListItem(content));
        InitEnvironment();
    }

    private void ExcelFileSelectButton_OnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls"
        };

        if (openFileDialog.ShowDialog() != true) return;
        var fileName = openFileDialog.FileName;
        Logger.Debug($"文件路径：{fileName}");
        ApplicationContext.FileName = fileName;
        SelectedFileNameTextBox.Text = Path.GetFileName(fileName);
    }


    private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
    {
        InitEnvironment();
    }

    private static void InitEnvironment()
    {
        if (Environment.Init())
        {
            MessageBox.Show("程序配置环境初始化成功", "初始化", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("程序配置环境初始化失败", "初始化", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ImportButton_OnClick(object sender, RoutedEventArgs e)
    {
        var importService = new ImportService();
        importService.Import(ApplicationContext.FileName);
    }

    private void UsernameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ApplicationContext.Username = UsernameTextBox.Text;
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        ApplicationContext.Password = PasswordBox.Password;
    }
}