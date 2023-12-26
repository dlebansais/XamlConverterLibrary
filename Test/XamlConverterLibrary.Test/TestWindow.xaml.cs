namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class TestWindow : Window
{
    public TestWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
}
