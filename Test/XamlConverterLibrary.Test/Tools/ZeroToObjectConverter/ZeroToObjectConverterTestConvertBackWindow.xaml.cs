namespace XamlConverterLibrary.Test;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class ZeroToObjectConverterTestConvertBackWindow : Window
{
    public ZeroToObjectConverterTestConvertBackWindow()
    {
        InitializeComponent();
        DataContext = new ZeroToObjectConverterTestClass();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        IntPropertyOne.SetCurrentValue(TextBox.TextProperty, "Visible");
        IntPropertyZero.SetCurrentValue(TextBox.TextProperty, "Collapsed");
        NullableIntProperty.SetCurrentValue(TextBox.TextProperty, "Collapsed");
        NullableIntPropertyOne.SetCurrentValue(TextBox.TextProperty, "Visible");
        NullableIntPropertyZero.SetCurrentValue(TextBox.TextProperty, "Collapsed");
    }
}
