namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class NullToObjectConverterTestConvertWindow : Window
{
    public NullToObjectConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new NullToObjectConverterTestClass();
    }
}
