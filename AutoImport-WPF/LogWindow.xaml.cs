using System.Collections.ObjectModel;

namespace AutoImport_WPF;

public partial class LogWindow
{
    private ObservableCollection<string> Logs { get; } = [];

    public LogWindow()
    {
        InitializeComponent();
        LogListBox.ItemsSource = Logs;
    }

    public void AddListItem(string content)
    {
        Logs.Add(content);
    }
}