﻿namespace Converters
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;
    using Contracts;

    /// <summary>
    /// Converter from a reference to the first or second object of a collection.
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instanciated in Xaml")]
    public class NullToObjectConverter : IValueConverter
    {
        /// <summary>
        /// Converter from a reference to the first or second object of a collection.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// If <paramref name="value"/> is non-null reference, and <paramref name="parameter"/> is a collection with at least two objects, the converter returns the second object in the collection.
        /// Otherwise, if collection has at least two objects, the converter returns the first object in the collection.
        /// Otherwise, this method throws an exception.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter);
        }

        /// <summary>
        /// Converter from a reference to the first or second object of a collection.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <returns>
        /// If <paramref name="value"/> is non-null reference, and <paramref name="parameter"/> is a collection with at least two objects, the converter returns the second object in the collection.
        /// Otherwise, if collection has at least two objects, the converter returns the first object in the collection.
        /// Otherwise, this method throws an exception.
        /// </returns>
        public static object Convert(object value, object parameter)
        {
            bool NullValue = value == null;

            if (parameter is IList CollectionOfItems && CollectionOfItems.Count > 1)
            {
                Contract.RequireNotNull(NullValue ? CollectionOfItems[0] : CollectionOfItems[1], out object Item);
                return Item;
            }
            else
                throw new ArgumentOutOfRangeException(nameof(parameter));
        }

        /// <summary>
        /// Converts an object to an <see cref="object"/> reference in a binding.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value.</returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is IList CollectionOfItems && CollectionOfItems.Count > 1)
                return value == CollectionOfItems[1] ? BindingOperations.DisconnectedSource : null;
            else
                throw new ArgumentOutOfRangeException(nameof(parameter));
        }
    }
}
