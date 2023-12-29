namespace XamlConverterLibrary.Test;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class NullToObjectConverterTestConvertBackWindow : Window
{
    public NullToObjectConverterTestConvertBackWindow()
    {
        InitializeComponent();
        DataContext = new NullToObjectConverterTestClass();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        StringPropertyNull.SetCurrentValue(TextBox.TextProperty, "Collapsed");
        StringPropertyNotNull.SetCurrentValue(TextBox.TextProperty, "Visible");
        IntPropertyNull.SetCurrentValue(TextBox.TextProperty, "Collapsed");
        IntPropertyNotNull.SetCurrentValue(TextBox.TextProperty, "Visible");
    }
}
