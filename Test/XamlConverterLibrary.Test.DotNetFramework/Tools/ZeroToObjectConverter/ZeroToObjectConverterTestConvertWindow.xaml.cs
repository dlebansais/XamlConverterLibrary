namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class ZeroToObjectConverterTestConvertWindow : Window
{
    public ZeroToObjectConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new ZeroToObjectConverterTestClass();
    }
}
