namespace XamlConverterLibrary.Test;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
internal partial class BooleanToObjectConverterTestConvertBackWindow : Window
{
    public BooleanToObjectConverterTestConvertBackWindow()
    {
        InitializeComponent();
        DataContext = new BooleanToObjectConverterTestClass();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        BoolPropertyTrue.SetCurrentValue(TextBox.TextProperty, "Visible");
        BoolPropertyFalse.SetCurrentValue(TextBox.TextProperty, "Collapsed");
        NullableBoolProperty.SetCurrentValue(TextBox.TextProperty, "Collapsed");
        NullableBoolPropertyTrue.SetCurrentValue(TextBox.TextProperty, "Visible");
        NullableBoolPropertyFalse.SetCurrentValue(TextBox.TextProperty, "Collapsed");
    }
}
