namespace Converters;

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Contracts;

/// <summary>
/// Provides helper methods for converters in this namespace.
/// </summary>
internal static class ConverterTools
{
    /// <summary>
    /// Checks whether the provided type is nullable.
    /// </summary>
    /// <param name="targetType">The type.</param>
    /// <returns><see langword="true"/> if <paramref name="targetType"/> is nullable; Otherwise, <see langword="false"/>.</returns>
    public static bool IsNullable(this Type targetType)
    {
        Contract.RequireNotNull(targetType, out Type TargetType);

        if (TargetType.IsGenericType)
        {
            Type GenericTypeDefinition = TargetType.GetGenericTypeDefinition();
            if (GenericTypeDefinition == typeof(Nullable<>))
                return true;
        }

        if (!TargetType.IsValueType)
            return true;

        return false;
    }

    /// <summary>
    /// Checks whether creating an instance of the provided type is possible.
    /// </summary>
    /// <param name="targetType">The type.</param>
    /// <returns><see langword="true"/> if creating an instance of <paramref name="targetType"/> is possible; Otherwise, <see langword="false"/>.</returns>
    /// <remarks>This method has a side effect and saves the new instance of <paramref name="targetType"/> in thread-local storage if successful.</remarks>
    public static bool CanCreateInstanceOf(this Type targetType)
    {
        Contract.RequireNotNull(targetType, out Type TargetType);

        var Fields = TargetType.GetFields();
        var StaticFieldInfo = Fields?.FirstOrDefault(IsStaticInstance);

        var Constructors = TargetType.GetConstructors();
        var ParameterlessConstructorInfo = Constructors?.FirstOrDefault((ConstructorInfo constructor) => constructor.GetParameters().Length == 0);

        object? NullableInstance = null;

        if (StaticFieldInfo is not null)
            NullableInstance = StaticFieldInfo.GetValue(null);

        if (ParameterlessConstructorInfo is not null)
            NullableInstance = ParameterlessConstructorInfo.Invoke(Array.Empty<object>());

        if (IsNullableValueType(TargetType, out Type ValueType))
        {
            ConstructorInfo[] ValidConstructors = Contract.AssertNotNull(Constructors);
            Contract.Require(ValidConstructors.Length == 1);

            ConstructorInfo ValueConstructorInfo = ValidConstructors[0];
            ParameterInfo[] Parameters = ValueConstructorInfo.GetParameters();
            Contract.Require(Parameters.Length == 1);

            ParameterInfo ValueParameter = Parameters[0];
            Contract.Require(ValueParameter.ParameterType == ValueType);

            var CreateInstanceOfValueType = () => Activator.CreateInstance(ValueType);
            object DefaultValueParameter = Contract.AssertNotNull(Contract.AssertNoThrow(CreateInstanceOfValueType));
            NullableInstance = ValueConstructorInfo.Invoke(new object[] { DefaultValueParameter });
        }

        if (NullableInstance is null)
            return false;
        else
        {
            LastInstance = NullableInstance;
            return true;
        }
    }

    private static bool IsNullableValueType(Type targetType, out Type valueType)
    {
        if (targetType.IsGenericType)
        {
            Type GenericTypeDefinition = targetType.GetGenericTypeDefinition();
            if (GenericTypeDefinition == typeof(Nullable<>))
            {
                Type[] GenericArguments = targetType.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 1);

                Type FirstGenericArgument = GenericArguments[0];
                Debug.Assert(FirstGenericArgument.IsValueType);

                valueType = FirstGenericArgument;
                return true;
            }
        }

        Contract.Unused(out valueType);
        return false;
    }

    private static bool IsStaticInstance(FieldInfo field)
    {
        var Attributes = field.Attributes;

        if (!Attributes.HasFlag(FieldAttributes.Static))
            return false;

        if (!Attributes.HasFlag(FieldAttributes.InitOnly))
            return false;

        return true;
    }

    /// <summary>
    /// Gets the object instance created by <see cref="CanCreateInstanceOf(Type)"/>.
    /// </summary>
    /// <returns>The object instance created by <see cref="CanCreateInstanceOf(Type)"/>.</returns>
    /// <remarks>Reading this property is valid if and only if <see cref="CanCreateInstanceOf(Type)"/> returned true.</remarks>
    public static object LastInstance
    {
        get { return Contract.AssertNotNull(LastInstanceInternal.Value); }
        private set { LastInstanceInternal.Value = value; }
    }

    private static readonly ThreadLocal<object> LastInstanceInternal = new();
}
