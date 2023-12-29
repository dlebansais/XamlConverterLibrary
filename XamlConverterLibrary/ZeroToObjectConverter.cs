namespace Converters;

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter from an int (or convertible) or nullable int to the first or second item in a collection.
/// </summary>
[ValueConversion(typeof(int), typeof(object))]
[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instanciated in Xaml")]
public class ZeroToObjectConverter : IValueConverter
{
    /// <summary>
    /// Converter from an int (or convertible) to the first or second object of a collection.
    /// Converter from a nullable int to the first, second or third object of a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be convertible to <see cref="int"/> or <see cref="Nullable{Int}"/>.</param>
    /// <param name="targetType">The type of the binding target property (ignored).</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is <see langword="null"/>, returns the third item.
    /// Otherwise, if <paramref name="value"/> is a <see cref="int"/> (or convertible) and non-zero, or a <see cref="Nullable{Int}"/> and also non-zero, returns the second item in the collection.
    /// Otherwise, returns the first item in the collection.
    /// </returns>
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, parameter);
    }

    /// <summary>
    /// Converter from an int (or convertible) to the first or second object of a collection.
    /// Converter from a nullable int to the first, second or third object of a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be <see cref="Nullable{Int}"/> or convertible to <see cref="int"/>.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is <see langword="null"/>, returns the third item.
    /// Otherwise, if <paramref name="value"/> is a <see cref="int"/> (or convertible) and non-zero, or a <see cref="Nullable{Int}"/> and also non-zero, returns the second item in the collection.
    /// Otherwise, returns the first item in the collection.
    /// </returns>
    public static object Convert(object? value, object parameter)
    {
        Contract.RequireNotNull(parameter, out IList Items);
        Contract.Require(Items.Count >= 2);

        if (value is null)
        {
            int Index = Items.Count > 2 ? 2 : 0;

            Contract.RequireNotNull(Items[Index], out object Item);

            return Item;
        }
        else
        {
            int IntValue = 0;

            bool IsConvertibleFromNumeric = ConvertValueFromNumeric(value, out int IntValueFromNumeric);
            if (IsConvertibleFromNumeric)
                IntValue = IntValueFromNumeric;

            bool IsConvertibleFromNullableSmallNumeric = ConvertValueFromNullableSmallNumeric(value, out int IntValueFromNullableSmallNumeric);
            if (IsConvertibleFromNullableSmallNumeric)
                IntValue = IntValueFromNullableSmallNumeric;

            bool IsConvertibleFromNullableLargeNumeric = ConvertValueFromNullableLargeNumeric(value, out int IntValueFromNullableLargeNumeric);
            if (IsConvertibleFromNullableLargeNumeric)
                IntValue = IntValueFromNullableLargeNumeric;

            bool IsConvertibleFromOtherTypes = ConvertValueFromOtherTypes(value, out int IntValueFromOtherTypes);
            if (IsConvertibleFromOtherTypes)
                IntValue = IntValueFromOtherTypes;

            bool IsConvertibleToInt = IsConvertibleFromNumeric || IsConvertibleFromNullableSmallNumeric || IsConvertibleFromNullableLargeNumeric || IsConvertibleFromOtherTypes;
            Contract.Require(IsConvertibleToInt);

            Contract.RequireNotNull(IntValue != 0 ? Items[1] : Items[0], out object Item);

            return Item;
        }
    }

    private static bool ConvertValueFromNumeric(object? value, out int intValue)
    {
        if (value is int AsInt)
            intValue = AsInt;
        else if (value is byte AsByte)
            intValue = AsByte;
        else if (value is sbyte AsSByte)
            intValue = AsSByte;
        else if (value is short AsShort)
            intValue = AsShort;
        else if (value is ushort AsUShort)
            intValue = AsUShort;
        else if (value is uint AsUInt)
            intValue = (int)AsUInt;
        else if (value is long AsLong)
            intValue = (int)AsLong;
        else if (value is ulong AsULong)
            intValue = (int)AsULong;
        else
        {
            intValue = 0;
            return false;
        }

        return true;
    }

    private static bool ConvertValueFromNullableSmallNumeric(object? value, out int intValue)
    {
        if (value is byte?)
            intValue = ((byte?)value) ?? 0;
        else if (value is sbyte?)
            intValue = ((sbyte?)value) ?? 0;
        else if (value is short?)
            intValue = ((short?)value) ?? 0;
        else if (value is ushort?)
            intValue = ((ushort?)value) ?? 0;
        else
        {
            intValue = 0;
            return false;
        }

        return true;
    }

    private static bool ConvertValueFromNullableLargeNumeric(object? value, out int intValue)
    {
        if (value is int?)
            intValue = ((int?)value) ?? 0;
        else if (value is uint?)
            intValue = (int)(((uint?)value) ?? 0);
        else if (value is long?)
            intValue = (int)(((long?)value) ?? 0);
        else if (value is ulong?)
            intValue = (int)(((ulong?)value) ?? 0);
        else
        {
            intValue = 0;
            return false;
        }

        return true;
    }

    private static bool ConvertValueFromOtherTypes(object value, out int intValue)
    {
        if (value is string AsString)
            intValue = AsString.Length;
        else if (value.GetType().IsEnum)
            intValue = (int)value;
        else if (value is IEnumerable AsEnumerable)
            intValue = AsEnumerable.GetEnumerator().MoveNext() ? 1 : 0;
        else
        {
            intValue = 0;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Converts an object to an <see cref="int"/> instance in a binding.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to. It must be convertible from <see cref="int"/> or <see cref="Nullable{Int}"/>.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is equal to the third item in the collection, returns <see langword="null"/>.
    /// Otherwise, if <paramref name="value"/> is equal to the second item in the collection, returns 1.
    /// Otherwise, returns 0.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertBack(value, parameter);
    }

    /// <summary>
    /// Converts an object to an <see cref="int"/> instance in a binding.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is equal to the third item in the collection, returns <see langword="null"/>.
    /// Otherwise, if <paramref name="value"/> is equal to the second item in the collection, returns 1.
    /// Otherwise, returns 0.
    /// </returns>
    public static object? ConvertBack(object value, object parameter)
    {
        Contract.RequireNotNull(value, out object Value);
        Contract.RequireNotNull(parameter, out IList Items);
        Contract.Require(Items.Count >= 2);

        if (Items.Count > 2 && Value.Equals(Items[2]))
            return null;
        else
            return Value.Equals(Items[1]) ? 1 : 0;
    }
}
