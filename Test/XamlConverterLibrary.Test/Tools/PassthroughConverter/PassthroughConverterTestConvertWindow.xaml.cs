namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class PassthroughConverterTestConvertWindow : Window
{
    public PassthroughConverterTestConvertWindow()
    {
        InitializeComponent();

        PassthroughConverterTestClass TestClassInstance = new()
        {
            StringProperty = "Test"
        };

        DataContext = TestClassInstance;
    }
}
