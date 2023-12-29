namespace Converters;

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter from a boolean or nullable boolean to an item in a collection.
/// </summary>
[ValueConversion(typeof(bool), typeof(object))]
[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instanciated in Xaml")]
public class BooleanToObjectConverter : IValueConverter
{
    /// <summary>
    /// Converter from a boolean to the first or second object of a collection.
    /// Converter from a nullable boolean to the first, second or third object of a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be either <see cref="bool"/> or <see cref="Nullable{Boolean}"/>.</param>
    /// <param name="targetType">The type of the binding target property (ignored).</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is <see langword="null"/>, returns the third item.
    /// Otherwise, if <paramref name="value"/> is <see langword="true"/>, returns the second item in the collection.
    /// Otherwise, returns the first item in the collection.
    /// </returns>
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, parameter);
    }

    /// <summary>
    /// Converter from a boolean to the first or second object of a collection.
    /// Converter from a nullable boolean to the first, second or third object of a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be either <see cref="bool"/> or <see cref="Nullable{Boolean}"/>.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is <see langword="null"/>, returns the third item.
    /// Otherwise, if <paramref name="value"/> is <see langword="true"/>, returns the second item in the collection.
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
            Contract.Require(value.GetType() == typeof(bool));

            bool BooleanValue = (bool)value;

            Contract.RequireNotNull(BooleanValue ? Items[1] : Items[0], out object Item);

            return Item;
        }
    }

    /// <summary>
    /// Converts an object to a <see cref="bool"/> instance in a binding.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to. It must be either <see cref="bool"/> or <see cref="Nullable{Boolean}"/>.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is equal to the third item in the collection, returns <see langword="null"/>.
    /// Otherwise, if <paramref name="value"/> is equal to the second item in the collection, returns <see langword="true"/>.
    /// Otherwise, returns <see langword="false"/>.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Contract.Require(targetType == typeof(bool) || targetType == typeof(bool?));

        return ConvertBack(value, parameter);
    }

    /// <summary>
    /// Converts an object to a <see cref="bool"/> instance in a binding.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <returns>
    /// If <paramref name="parameter"/> is a collection with at least three items, and <paramref name="value"/> is equal to the third item in the collection, returns <see langword="null"/>.
    /// Otherwise, if <paramref name="value"/> is equal to the second item in the collection, returns <see langword="true"/>.
    /// Otherwise, returns <see langword="false"/>.
    /// </returns>
    public static object? ConvertBack(object value, object parameter)
    {
        Contract.RequireNotNull(value, out object Value);
        Contract.RequireNotNull(parameter, out IList Items);
        Contract.Require(Items.Count >= 2);

        if (Items.Count > 2 && Value.Equals(Items[2]))
            return null;
        else
            return Value.Equals(Items[1]);
    }
}
