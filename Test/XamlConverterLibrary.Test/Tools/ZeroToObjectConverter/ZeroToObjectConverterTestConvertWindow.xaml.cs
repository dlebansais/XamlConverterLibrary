namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class ZeroToObjectConverterTestConvertWindow : Window
{
    public ZeroToObjectConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new ZeroToObjectConverterTestClass();
    }
}
