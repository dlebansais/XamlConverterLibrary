namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class BooleanToObjectConverterTestConvertWindow : Window
{
    public BooleanToObjectConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new BooleanToObjectConverterTestClass();
    }
}
