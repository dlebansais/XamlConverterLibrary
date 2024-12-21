#pragma warning disable CA1812 // Avoid uninstantiated internal classes. This class is instantiated in XAML.

namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;

internal class SampleOneWayOnlyConverter : OneWayOnlyConverter, IValueConverter
{
    public override object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        => value is null ? throw new ArgumentNullException(nameof(value)) : value;
}
