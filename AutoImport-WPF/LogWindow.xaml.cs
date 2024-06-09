using System.Collections.ObjectModel;
using System.Windows;

namespace AutoImport_WPF;

public partial class LogWindow : Window
{
    private ObservableCollection<string> Logs { get; }

    public LogWindow()
    {
        InitializeComponent();
        Logs = new ObservableCollection<string>();
        LogListBox.ItemsSource = Logs;
    }

    public void AddListItem(string content)
    {
        Logs.Add(content);
    }
}