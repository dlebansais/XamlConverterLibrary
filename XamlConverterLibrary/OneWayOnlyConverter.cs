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

    /// <summary>
    /// Throws <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="value"><paramref name="value"/> is not used.</param>
    /// <param name="targetType"><paramref name="targetType"/> is not used.</param>
    /// <param name="parameter"><paramref name="parameter"/> is not used.</param>
    /// <param name="culture"><paramref name="culture"/> is not used.</param>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
