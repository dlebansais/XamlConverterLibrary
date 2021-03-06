﻿namespace Converters
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;
    using Contracts;

    /// <summary>
    /// Converter from a boolean or nullable boolean to the first or second object of a collection.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(object))]
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instanciated in Xaml")]
    public class BooleanToObjectConverter : IValueConverter
    {
        /// <summary>
        /// Converter from a boolean or nullable boolean to the first or second object of a collection.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// If <paramref name="parameter"/> is a collection with at least three objects, and <paramref name="value"/> is null, returns the 3rd item.
        /// Otherwise, If <paramref name="value"/> is a bool and equal to True, or a bool? and also equal to true, and <paramref name="parameter"/> is a collection with at least two objects, the converter returns the second object in the collection.
        /// Otherwise, if the collection has at least two objects, the converter returns the first object in the collection.
        /// Otherwise, this method throws an exception.
        /// </returns>
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter);
        }

        /// <summary>
        /// Converter from a boolean or nullable boolean to the first or second object of a collection.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <returns>
        /// If <paramref name="parameter"/> is a collection with at least three objects, and <paramref name="value"/> is null, returns the 3rd item.
        /// Otherwise, If <paramref name="value"/> is a bool and equal to True, or a bool? and also equal to true, and <paramref name="parameter"/> is a collection with at least two objects, the converter returns the second object in the collection.
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

            bool BooleanValue;

            if (value is bool)
                BooleanValue = (bool)value;
            else if (value is bool?)
                BooleanValue = ((bool?)value) ?? false;
            else
                throw new ArgumentOutOfRangeException(nameof(value));

            if (parameter is IList CollectionOfItems && CollectionOfItems.Count > 1)
            {
                Contract.RequireNotNull(BooleanValue ? CollectionOfItems[1] : CollectionOfItems[0], out object Item);
                return Item;
            }
            else
                throw new ArgumentOutOfRangeException(nameof(parameter));
        }

        /// <summary>
        /// Converts an object to a <see cref="bool"/> instance in a binding.
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
                return value == CollectionOfItems[1];
            else
                throw new ArgumentOutOfRangeException(nameof(parameter));
        }
    }
}
