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
public class NullToObjectConverter : IValueConverter
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
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing exactly two items.</param>
    /// <returns>
    /// If <paramref name="value"/> is not <see langword="null"/>, the converter returns the second item in the collection.
    /// Otherwise, the converter returns the first item in the collection.
    /// </returns>
    public static object Convert(object value, object parameter)
    {
        Contract.RequireNotNull(parameter, out IList Items);
        Contract.Require(Items.Count == 2);

        bool IsNullValue = value is null;

        Contract.RequireNotNull(IsNullValue ? Items[0] : Items[1], out object Item);
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
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Contract.RequireNotNull(targetType, out Type TargetType);
        Contract.Require(TargetType.IsNullable());
        Contract.Require(targetType.CanCreateInstanceOf());

        // This instance is obtained as a side effect of the call to CanCreateInstanceOf(targetType).
        object Instance = Contract.NullSupressed(ConverterTools.LastInstance);

        return ConvertBack(value, parameter, Instance);
    }

    /// <summary>
    /// Converts an object to an <see cref="object"/> reference in a binding.
    /// </summary>
    /// <typeparam name="T">The type of the returned object, if not <see langword="null"/>.</typeparam>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="parameter">The converter parameter to use. It must be a collection of objects containing exactly two items.</param>
    /// <param name="instance">The instance to return if the result of the conversion is not <see langword="null"/>.</param>
    /// <returns>
    /// If <paramref name="value"/> is equal to the second item in the collection, returns <paramref name="instance"/>.
    /// Otherwise, returns <see langword="null"/>.
    /// </returns>
    public static T? ConvertBack<T>(object value, object parameter, T instance)
        where T : class
    {
        Contract.RequireNotNull(value, out object Value);
        Contract.RequireNotNull(parameter, out IList Items);
        Contract.Require(Items.Count == 2);
        Contract.RequireNotNull(instance, out T Instance);

        return Value.Equals(Items[1]) ? Instance : null;
    }
}
