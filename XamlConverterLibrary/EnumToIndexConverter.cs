namespace Converters;

using System;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter to and from enum to int.
/// </summary>
[ValueConversion(typeof(Enum), typeof(int))]
public class EnumToIndexConverter : IValueConverter
{
    /// <summary>
    /// Converter from an enum value to its index in the enumeration.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be an enum.</param>
    /// <param name="targetType">The type of the binding target property (ignored).</param>
    /// <param name="parameter">The converter parameter to use (ignored).</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>The index of <paramref name="value"/> in the enumeration.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Contract.Require(value is Enum);

        return Convert((Enum)value);
    }

    /// <summary>
    /// Converter from an enum value to its index in the enumeration.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be an enum.</param>
    /// <returns>The index of <paramref name="value"/> in the enumeration.</returns>
    public static object Convert(Enum value)
    {
        Contract.RequireNotNull(value, out Enum Value);

        Array Values = Value.GetType().GetEnumValues();
        int Index = -1;

        for (int i = 0; i < Values.Length; i++)
            if (Value.Equals(Values.GetValue(i)))
            {
                Index = i;
                break;
            }

        Contract.Require(Index >= 0 && Index < Values.Length);

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
    public static object ConvertBack(object value, Type targetType)
    {
        Contract.RequireNotNull(value, out object Value);
        Contract.Require(typeof(int).IsAssignableFrom(Value.GetType()));
        Contract.RequireNotNull(targetType, out Type TargetType);
        Contract.Require(typeof(Enum).IsAssignableFrom(TargetType));

        int Index = (int)value;
        Array EnumValues = TargetType.GetEnumValues();

        Contract.Require(Index >= 0 && Index < EnumValues.Length);

        object? EnumValue = EnumValues.GetValue(Index);

        Contract.Require(EnumValue is not null);

        return EnumValue!;
    }
}
