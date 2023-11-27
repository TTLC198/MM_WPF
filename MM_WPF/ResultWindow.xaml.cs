using System.Windows;

namespace MM_WPF;

public partial class ResultWindow : Window
{
    public string Message { get; set; }
        
    public ResultWindow(string message)
    {
        Message = message;
        InitializeComponent();
    }
}