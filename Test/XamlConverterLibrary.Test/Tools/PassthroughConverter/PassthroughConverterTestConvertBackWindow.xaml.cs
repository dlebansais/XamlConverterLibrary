namespace XamlConverterLibrary.Test;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class PassthroughConverterTestConvertBackWindow : Window
{
    public PassthroughConverterTestConvertBackWindow()
    {
        InitializeComponent();
        DataContext = new PassthroughConverterTestClass();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e) => StringProperty.SetCurrentValue(TextBox.TextProperty, "Test");
}
