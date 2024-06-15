using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.log;
using AutoImport_WPF.log.logger;
using AutoImport_WPF.service;
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

    private IImportService ImportService { get; } = new ChromeImportService();

    public MainWindow()
    {
        InitializeComponent();
        var logWindow = new LogWindow();
        logWindow.Show();
        LogConfig.Logger = new CommonLogger(content => logWindow.AddListItem(content));
        InitEnvironment();
        SetRememberUserConfig();
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

    private void SetRememberUserConfig()
    {
        var username = UserConfig.GetUsername();
        if (username != null)
        {
            UsernameTextBox.Text = username;
            ApplicationContext.Username = username;
        }

        var password = UserConfig.GetPassword();
        if (password == null) return;
        PasswordBox.Password = password;
        ApplicationContext.Password = password;
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

    private void UsernameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        // 320922197806044437
        var username = UsernameTextBox.Text;
        ApplicationContext.Username = username;
        UserConfig.SaveUsername(username);
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        // bh88888888
        var password = PasswordBox.Password;
        ApplicationContext.Password = password;
        UserConfig.SavePassword(password);
    }

    private void ImportContractButton_OnClick(object sender, RoutedEventArgs e)
    {
        Logger.Info("开始导入签约服务");
        ImportService.ImportContract(ApplicationContext.FileName);
        Logger.Info("导入签约服务结束");
    }

    private void ImportHealthFormButton_OnClick(object sender, RoutedEventArgs e)
    {
        var importService = new ChromeImportService();
        importService.Import(ApplicationContext.FileName);
    }

    private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        Application.Current.Shutdown();
    }
}