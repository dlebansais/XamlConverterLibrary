namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter that leaves a value unchanged as long as it is not <see langword="null"/>.
/// </summary>
[ValueConversion(typeof(object), typeof(object))]
public class PassthroughConverter : IValueConverter
{
    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <param name="value">The value produced by the binding source. Must not be <see langword="null"/>.</param>
    /// <param name="targetType">The type of the binding target property (ignored).</param>
    /// <param name="parameter">The converter parameter to use (ignored).</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns><paramref name="value"/>.</returns>
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value);
    }

    /// <summary>
    /// Converts a value levaing it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <param name="value">The value produced by the binding source. Must not be <see langword="null"/>.</param>
    /// <returns><paramref name="value"/>.</returns>
    public static object Convert(object? value)
    {
        Contract.RequireNotNull(value, out object Value);

        return Value;
    }

    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target. Must not be <see langword="null"/>.</param>
    /// <param name="targetType">The type to convert to (ignored).</param>
    /// <param name="parameter">The converter parameter to use (ignored).</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns><paramref name="value"/>.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertBack(value);
    }

    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target. Must not be <see langword="null"/>.</param>
    /// <returns><paramref name="value"/>.</returns>
    public static object ConvertBack(object value)
    {
        Contract.RequireNotNull(value, out object Value);

        return Value;
    }
}
