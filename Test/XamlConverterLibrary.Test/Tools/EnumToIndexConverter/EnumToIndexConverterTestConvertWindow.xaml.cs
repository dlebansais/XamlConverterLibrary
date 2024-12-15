namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class EnumToIndexConverterTestConvertWindow : Window
{
    public EnumToIndexConverterTestConvertWindow()
    {
        InitializeComponent();
        DataContext = new EnumToIndexConverterTestClass();
    }
}
