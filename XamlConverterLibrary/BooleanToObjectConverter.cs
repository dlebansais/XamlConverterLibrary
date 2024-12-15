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
public partial class BooleanToObjectConverter : IValueConverter
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
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture) => Convert(value, parameter);

    /// <summary>
    /// Converter from a boolean to the first or second object of a collection.
    /// Converter from a nullable boolean to the first, second or third object of a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be either <see cref="bool"/> or <see cref="Nullable{Boolean}"/>.</param>
    /// <param name="items">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <returns>
    /// If <paramref name="items"/> is a collection with at least three items, and <paramref name="value"/> is <see langword="null"/>, returns the third item.
    /// Otherwise, if <paramref name="value"/> is <see langword="true"/>, returns the second item in the collection.
    /// Otherwise, returns the first item in the collection.
    /// </returns>
    [RequireNotNull(nameof(items), Type = "object", Name = "parameter", AliasName = "Items")]
    [Require("value is null || value.GetType() == typeof(bool)")]
    [Require("Items.Count >= 2")]
    private static object ConvertVerified(object? value, IList items)
    {
        if (value is null)
        {
            int Index = items.Count > 2 ? 2 : 0;

            object Item = Contract.AssertNotNull(items[Index]);

            return Item;
        }
        else
        {
            bool BooleanValue = (bool)value;

            object Item = Contract.AssertNotNull(BooleanValue ? items[1] : items[0]);

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
    [Access("public")]
    [Require("targetType == typeof(bool) || targetType == typeof(bool?)")]
#pragma warning disable IDE0060 // Remove unused parameter
    private static object? ConvertBackVerified(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, parameter);
#pragma warning restore IDE0060 // Remove unused parameter

    /// <summary>
    /// Converts an object to a <see cref="bool"/> instance in a binding.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="items">The converter parameter to use. It must be a collection of objects containing at least two items.</param>
    /// <returns>
    /// If <paramref name="items"/> is a collection with at least three items, and <paramref name="value"/> is equal to the third item in the collection, returns <see langword="null"/>.
    /// Otherwise, if <paramref name="value"/> is equal to the second item in the collection, returns <see langword="true"/>.
    /// Otherwise, returns <see langword="false"/>.
    /// </returns>
    [RequireNotNull(nameof(value))]
    [RequireNotNull(nameof(items), Type = "object", Name = "parameter", AliasName = "Items")]
    [Require("Items.Count >= 2")]
    private static object? ConvertBackVerified(object value, IList items)
    {
        object? Result = items.Count > 2 && value.Equals(items[2]) ? null : value.Equals(items[1]);
        return Result;
    }
}
