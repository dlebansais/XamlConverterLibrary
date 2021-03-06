﻿namespace Converters
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;
    using Contracts;

    /// <summary>
    /// Converter from an int (or convertible) or nullable int to the first or second object of a collection.
    /// </summary>
    [ValueConversion(typeof(int), typeof(object))]
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instanciated in Xaml")]
    public class ZeroToObjectConverter : IValueConverter
    {
        /// <summary>
        /// Converter from an int (or convertible) or nullable int to the first or second object of a collection.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// If <paramref name="parameter"/> is a collection with at least three objects, and <paramref name="value"/> is null, returns the 3rd item.
        /// Otherwise, If <paramref name="value"/> is an int (or convertible) and non-zero, or an int? and also non-zero, and <paramref name="parameter"/> is a collection with at least two objects, the converter returns the second object in the collection.
        /// Otherwise, if the collection has at least two objects, the converter returns the first object in the collection.
        /// Otherwise, this method throws an exception.
        /// </returns>
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter);
        }

        /// <summary>
        /// Converter from an int (or convertible) or nullable int to the first or second object of a collection.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <returns>
        /// If <paramref name="parameter"/> is a collection with at least three objects, and <paramref name="value"/> is null, returns the 3rd item.
        /// Otherwise, If <paramref name="value"/> is an int (or convertible) and non-zero, or an int? and also non-zero, and <paramref name="parameter"/> is a collection with at least two objects, the converter returns the second object in the collection.
        /// Otherwise, if the collection has at least two objects, the converter returns the first object in the collection.
        /// Otherwise, this method throws an exception.
        /// </returns>
        public static object Convert(object? value, object parameter)
        {
            if (value == null && parameter is IList CollectionOfThreeItems && CollectionOfThreeItems.Count > 2)
            {
                Contract.RequireNotNull(CollectionOfThreeItems[2], out object Item);
                return Item;
            }

            int IntValue;

            if (!ConvertValueFromNumeric(value, out IntValue) &&
                !ConvertValueFromNullableSmallNumeric(value, out IntValue) &&
                !ConvertValueFromNullableLargeNumeric(value, out IntValue) &&
                !ConvertValueFromOtherTypes(value, out IntValue))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (parameter is IList CollectionOfItems && CollectionOfItems.Count > 1)
            {
                Contract.RequireNotNull(IntValue != 0 ? CollectionOfItems[1] : CollectionOfItems[0], out object Item);
                return Item;
            }
            else
                throw new ArgumentOutOfRangeException(nameof(parameter));
        }

        private static bool ConvertValueFromNumeric(object? value, out int intValue)
        {
            if (value is int)
                intValue = (int)value;
            else if (value is byte)
                intValue = (byte)value;
            else if (value is sbyte)
                intValue = (sbyte)value;
            else if (value is short)
                intValue = (short)value;
            else if (value is ushort)
                intValue = (ushort)value;
            else if (value is uint)
                intValue = (int)(uint)value;
            else if (value is long)
                intValue = (int)(long)value;
            else if (value is ulong)
                intValue = (int)(ulong)value;
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

        private static bool ConvertValueFromOtherTypes(object? value, out int intValue)
        {
            if (value == null)
                intValue = 0;
            else if (value is string AsString)
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
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is IList CollectionOfThreeItems && CollectionOfThreeItems.Count > 2 && value == CollectionOfThreeItems[2])
                return null;
            else if (parameter is IList CollectionOfItems && CollectionOfItems.Count > 1)
                return value == CollectionOfItems[1] ? 1 : 0;
            else
                throw new ArgumentOutOfRangeException(nameof(parameter));
        }
    }
}
