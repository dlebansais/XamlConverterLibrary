namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class NullToObjectConverterTestConvertWindow : Window
{
    public NullToObjectConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new NullToObjectConverterTestClass();
    }
}
