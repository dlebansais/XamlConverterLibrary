namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter that leaves a value unchanged as long as it is not <see langword="null"/>.
/// </summary>
[ValueConversion(typeof(object), typeof(object))]
public partial class PassthroughConverter : IValueConverter
{
    /// <inheritdoc cref="IValueConverter.Convert" />
    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <returns><paramref name="value"/>.</returns>
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        => value is null ? throw new ArgumentNullException(nameof(value)) : ConvertVerified(value);

    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <param name="value">The value produced by the binding source. Must not be <see langword="null"/>.</param>
    /// <returns><paramref name="value"/>.</returns>
    [Access("public", "static")]
    [RequireNotNull(nameof(value))]
    private static object ConvertVerified(object value) => value;

    /// <inheritdoc cref="IValueConverter.ConvertBack" />
    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <returns><paramref name="value"/>.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value);

    /// <summary>
    /// Converts a value leaving it unchanged as long as it is not <see langword="null"/>.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target. Must not be <see langword="null"/>.</param>
    /// <returns><paramref name="value"/>.</returns>
    [RequireNotNull(nameof(value))]
    private static object ConvertBackVerified(object value) => value;
}
