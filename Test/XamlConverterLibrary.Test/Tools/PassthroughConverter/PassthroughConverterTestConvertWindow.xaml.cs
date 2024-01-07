namespace XamlConverterLibrary.Test;

using System.Windows;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class PassthroughConverterTestConvertWindow : Window
{
    public PassthroughConverterTestConvertWindow()
    {
        InitializeComponent();

        PassthroughConverterTestClass TestClassInstance = new();
        TestClassInstance.StringProperty = "Test";

        DataContext = TestClassInstance;
    }
}
