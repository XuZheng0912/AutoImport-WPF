using System.Collections.ObjectModel;
using System.Windows;

namespace AutoImport_WPF;

public partial class LogWindow : Window
{
    public ObservableCollection<string> Logs { get; set; }

    public LogWindow()
    {
        InitializeComponent();
        Logs = new ObservableCollection<string>();
        DataContext = this;
    }

    public void Debug(string content)
    {
        Logs.Add(content);
    }
}