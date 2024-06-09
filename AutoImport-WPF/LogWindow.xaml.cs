using System.Collections.ObjectModel;
using System.Windows;
using AutoImport_WPF.config;
using static System.DateTime;

namespace AutoImport_WPF;

public partial class LogWindow : Window
{
    public LogLevel LogLevel { get; set; }
    private ObservableCollection<string> Logs { get; }


    public LogWindow()
    {
        InitializeComponent();
        Logs = new ObservableCollection<string>();
        LogListBox.ItemsSource = Logs;
    }

    public LogWindow(ObservableCollection<string> logs)
    {
        Logs = logs;
    }

    public bool EnableDebug()
    {
        return LogLevel >= LogLevel.Debug;
    }

    public bool EnableInfo()
    {
        return LogLevel > LogLevel.Info;
    }

    public bool EnableWarn()
    {
        return LogLevel > LogLevel.Warn;
    }

    public void Debug(string content)
    {
        if (EnableDebug())
        {
            Log("debug", content);
        }
    }

    private void Log(string mode, string content)
    {
        Logs.Add(LogContent(mode, content));
    }

    private static string LogContent(string mode, string content)
    {
        return $"[{mode}]-[{Now}]-{content}";
    }
}