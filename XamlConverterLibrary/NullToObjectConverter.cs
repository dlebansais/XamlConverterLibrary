namespace Converters;

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using Contracts;

/// <summary>
/// Converter from a nullable reference to the first or second item in a collection.
/// </summary>
[ValueConversion(typeof(object), typeof(object))]
[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Instanciated in Xaml")]
public partial class NullToObjectConverter : IValueConverter
{
    /// <summary>
    /// Converter from a nullable reference to the first or second item in a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be nullable.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing exactly two items.</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>
    /// If <paramref name="value"/> is not <see langword="null"/>, the converter returns the second item in the collection.
    /// Otherwise, the converter returns the first item in the collection.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, parameter);
    }

    /// <summary>
    /// Converter from a nullable reference to the first or second item in a collection.
    /// </summary>
    /// <param name="value">The value produced by the binding source. The type of <paramref name="value"/> must be nullable.</param>
    /// <param name="items">The converter parameter to use. It must be a collection of objects containing exactly two items.</param>
    /// <returns>
    /// If <paramref name="value"/> is not <see langword="null"/>, the converter returns the second item in the collection.
    /// Otherwise, the converter returns the first item in the collection.
    /// </returns>
    [RequireNotNull(nameof(items), Type = "object", Name = "parameter", AliasName = "Items")]
    [Require("Items.Count == 2")]
    private static object ConvertVerified(object value, IList items)
    {
        bool IsNullValue = value is null;

        object Item = Contract.AssertNotNull(IsNullValue ? items[0] : items[1]);

        return Item;
    }

    /// <summary>
    /// Converts an object to an <see cref="object"/> reference in a binding.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to. It must be a nullable type for which a default instance can be constructed. A class with a parameterless constructor, for instance.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing exactly two items.</param>
    /// <param name="culture">The culture to use in the converter (ignored).</param>
    /// <returns>
    /// If <paramref name="value"/> is equal to the second item in the collection, returns a new instance of <paramref name="targetType"/>.
    /// Otherwise, returns <see langword="null"/>.
    /// </returns>
    [Access("public")]
    [RequireNotNull(nameof(targetType))]
    [Require("TargetType.IsNullable()")]
    [Require("TargetType.CanCreateInstanceOf()")]
    private static object? ConvertBackVerified(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This instance is obtained as a side effect of the call to CanCreateInstanceOf(targetType).
        object Instance = Contract.AssertNotNull(ConverterTools.LastInstance);

        return ConvertBack(value, parameter, Instance);
    }

    /// <summary>
    /// Converts an object to an <see cref="object"/> reference in a binding.
    /// </summary>
    /// <typeparam name="T">The type of the returned object, if not <see langword="null"/>.</typeparam>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="items">The converter parameter to use. It must be a collection of objects containing exactly two items.</param>
    /// <param name="instance">The instance to return if the result of the conversion is not <see langword="null"/>.</param>
    /// <returns>
    /// If <paramref name="value"/> is equal to the second item in the collection, returns <paramref name="instance"/>.
    /// Otherwise, returns <see langword="null"/>.
    /// </returns>
    [RequireNotNull(nameof(value))]
    [RequireNotNull(nameof(items), Type = "object", Name = "parameter", AliasName = "Items")]
    [Require("Items.Count == 2")]
    [RequireNotNull(nameof(instance))]
    private static T? ConvertBackVerified<T>(object value, IList items, T instance)
        where T : class
    {
        return value.Equals(items[1]) ? instance : null;
    }
}
