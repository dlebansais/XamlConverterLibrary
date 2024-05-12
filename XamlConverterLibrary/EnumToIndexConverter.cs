namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter to and from enum to int.
/// </summary>
[ValueConversion(typeof(Enum), typeof(int))]
public partial class EnumToIndexConverter : IValueConverter
{
    /// <summary>
    /// Converter from an enum value to its index in the enumeration.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be an enum.</param>
    /// <param name="targetType">The type of the binding target property (ignored).</param>
    /// <param name="parameter">The converter parameter to use (ignored).</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>The index of <paramref name="value"/> in the enumeration.</returns>
    [Access("public")]
    [Require("value is Enum")]
    private static object ConvertVerified(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert((Enum)value);
    }

    /// <summary>
    /// Converter from an enum value to its index in the enumeration.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be an enum.</param>
    /// <returns>The index of <paramref name="value"/> in the enumeration.</returns>
    [RequireNotNull(nameof(value))]
    [Ensure("(int)Result >= 0 && (int)Result < Value.GetType().GetEnumValues().Length")]
    private static object ConvertVerified(Enum value)
    {
        Array Values = value.GetType().GetEnumValues();
        int Index = -1;

        for (int i = 0; i < Values.Length; i++)
            if (value.Equals(Values.GetValue(i)))
            {
                Index = i;
                break;
            }

        return Index;
    }

    /// <summary>
    /// Converter from the index of an enum to its value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target. It must be convertible to <see cref="int"/>.</param>
    /// <param name="targetType">The type to convert to. It must be an enum.</param>
    /// <param name="parameter">The converter parameter to use (ignored).</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>The enum value at the index provided by <paramref name="value"/>.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertBack(value, targetType);
    }

    /// <summary>
    /// Converter from the index of an enum to its value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to. It must be an enum.</param>
    /// <returns>The enum value at the index provided by <paramref name="value"/>.</returns>
    [RequireNotNull(nameof(value))]
    [Require("typeof(int).IsAssignableFrom(Value.GetType())")]
    [RequireNotNull(nameof(targetType))]
    [Require("typeof(Enum).IsAssignableFrom(TargetType)")]
    [Require("(int)Value >= 0 && (int)Value < TargetType.GetEnumValues().Length")]
    private static object ConvertBackVerified(object value, Type targetType)
    {
        int Index = (int)value;
        Array EnumValues = targetType.GetEnumValues();
        object? EnumValue = EnumValues.GetValue(Index);
        object Result = Contract.AssertNotNull(EnumValue);

        return Result;
    }
}
