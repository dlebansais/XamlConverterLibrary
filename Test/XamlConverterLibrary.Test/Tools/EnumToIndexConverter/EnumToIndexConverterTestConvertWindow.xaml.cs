namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class EnumToIndexConverterTestConvertWindow : Window
{
    public EnumToIndexConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new EnumToIndexConverterTestClass();
    }
}
