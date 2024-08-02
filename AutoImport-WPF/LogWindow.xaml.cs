using System.Collections.ObjectModel;

namespace AutoImport_WPF;

public partial class LogWindow
{
    private ObservableCollection<string> Logs { get; } = [];

    private LogWindow()
    {
        InitializeComponent();
        LogListBox.ItemsSource = Logs;
    }

    public static LogWindow Init()
    {
        return new LogWindow();
    }

    public void AddListItem(string content)
    {
        Logs.Add(content);
    }
}