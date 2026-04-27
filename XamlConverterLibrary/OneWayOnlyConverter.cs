namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;

/// <summary>
/// Represents a one-way only converter.
/// Descendants must implement the <see cref="IValueConverter.Convert"/> method.
/// The <see cref="IValueConverter.ConvertBack"/> method throws <see cref="NotSupportedException"/>.
/// </summary>
public abstract class OneWayOnlyConverter : IValueConverter
{
    /// <inheritdoc cref="IValueConverter.Convert" />
    public abstract object Convert(object? value, Type targetType, object parameter, CultureInfo culture);

    /// <inheritdoc cref="IValueConverter.ConvertBack" />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
