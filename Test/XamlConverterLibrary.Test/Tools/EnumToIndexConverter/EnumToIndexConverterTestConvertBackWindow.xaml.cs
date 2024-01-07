namespace XamlConverterLibrary.Test;

using System.Windows;
using System.Windows.Controls.Primitives;

/// <summary>
/// Interaction logic for TestWindow.xaml
/// </summary>
public partial class EnumToIndexConverterTestConvertBackWindow : Window
{
    public EnumToIndexConverterTestConvertBackWindow()
    {
        InitializeComponent();
        DataContext = new EnumToIndexConverterTestClass();
        Loaded += OnLoaded;

        EnumPropertyIndexZero.SetCurrentValue(Selector.SelectedIndexProperty, 0);
        EnumPropertyIndexOne.SetCurrentValue(Selector.SelectedIndexProperty, 0);
        EnumPropertyIndexTwo.SetCurrentValue(Selector.SelectedIndexProperty, 0);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        EnumPropertyIndexZero.SetCurrentValue(Selector.SelectedIndexProperty, 1);
        EnumPropertyIndexOne.SetCurrentValue(Selector.SelectedIndexProperty, 2);
        EnumPropertyIndexTwo.SetCurrentValue(Selector.SelectedIndexProperty, 0);
    }
}
