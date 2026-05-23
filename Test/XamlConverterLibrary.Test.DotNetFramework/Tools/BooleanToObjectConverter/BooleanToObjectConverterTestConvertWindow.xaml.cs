namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class BooleanToObjectConverterTestConvertWindow : Window
{
    public BooleanToObjectConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new BooleanToObjectConverterTestClass();
    }
}
