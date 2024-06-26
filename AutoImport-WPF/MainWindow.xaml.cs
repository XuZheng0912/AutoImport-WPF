using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using AutoImport_WPF.config;
using AutoImport_WPF.context;
using AutoImport_WPF.log;
using AutoImport_WPF.log.logger;
using AutoImport_WPF.service;
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
        InitializeLogWindow();
        InitEnvironment();
        SetRememberUserConfig();
    }

    private void InitializeLogWindow()
    {
        var logWindow = new LogWindow();
        logWindow.Show();
        LogConfig.Logger = new CommonLogger(content => Dispatcher.Invoke(() => logWindow.AddListItem(content)));
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
        var username = UsernameTextBox.Text;
        ApplicationContext.Username = username;
        UserConfig.SaveUsername(username);
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        var password = PasswordBox.Password;
        ApplicationContext.Password = password;
        UserConfig.SavePassword(password);
    }

    private async void ImportContractButton_OnClick(object sender, RoutedEventArgs e)
    {
        var beforeHandler = BeforeHandleImportButtonClick();
        if (beforeHandler != null)
        {
            beforeHandler();
            return;
        }

        Logger.Info("开始导入签约服务数据");
        await Task.Run(() => ImportServiceProvider.GetImportService().ImportContract(ApplicationContext.FileName));
        Logger.Info("导入签约服务数据结束");
    }


    private async void ImportHealthFormButton_OnClick(object sender, RoutedEventArgs e)
    {
        var beforeHandler = BeforeHandleImportButtonClick();
        if (beforeHandler != null)
        {
            beforeHandler();
            return;
        }

        Logger.Info("开始导入健康体检表数据");
        await Task.Run(() => ImportServiceProvider.GetImportService().ImportHealthForm(ApplicationContext.FileName));
        Logger.Info("导入健康体检表数据结束");
    }

    private static Action? BeforeHandleImportButtonClick()
    {
        List<Action?> beforeHandlers =
        [
            CheckIsUserNameFilled(),
            CheckIsPasswordFilled(),
            CheckIsFileSelected()
        ];
        return beforeHandlers.OfType<Action>().FirstOrDefault();
    }

    private static Action? CheckIsFileSelected()
    {
        return CheckIsFilled
        (
            () => "请选择导入文件",
            () => ApplicationContext.FileName
        );
    }

    private static Action? CheckIsPasswordFilled()
    {
        return CheckIsFilled
        (
            () => "请填写密码",
            () => ApplicationContext.Password
        );
    }

    private static Action? CheckIsUserNameFilled()
    {
        return CheckIsFilled
        (
            () => "请填写用户名",
            () => ApplicationContext.Username
        );
    }

    private static Action? CheckIsFilled(Func<string> getMessage, Func<string> getContent)
    {
        if (string.IsNullOrWhiteSpace(getContent()))
        {
            return () => MessageBox.Show(getMessage(), "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        return null;
    }

    private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        Application.Current.Shutdown();
    }
}