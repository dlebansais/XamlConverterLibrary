namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class OneWayOnlyConverterTestConvertWindow : Window
{
    public OneWayOnlyConverterTestConvertWindow()
    {
        InitializeComponent();

        OneWayOnlyConverterTestClass TestClassInstance = new()
        {
            StringProperty = "Test"
        };

        DataContext = TestClassInstance;
    }
}
