namespace Converters;

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Contracts;

/// <summary>
/// Provides helper methods for converters in this namespace.
/// </summary>
internal static partial class ConverterTools
{
    /// <summary>
    /// Checks whether the provided type is nullable.
    /// </summary>
    /// <param name="targetType">The type.</param>
    /// <returns><see langword="true"/> if <paramref name="targetType"/> is nullable; Otherwise, <see langword="false"/>.</returns>
    [RequireNotNull(nameof(targetType))]
    private static bool IsNullableVerified(this Type targetType)
    {
        if (targetType.IsGenericType)
        {
            Type GenericTypeDefinition = targetType.GetGenericTypeDefinition();
            if (GenericTypeDefinition == typeof(Nullable<>))
                return true;
        }

        return !targetType.IsValueType;
    }

    /// <summary>
    /// Checks whether creating an instance of the provided type is possible.
    /// </summary>
    /// <param name="targetType">The type.</param>
    /// <returns><see langword="true"/> if creating an instance of <paramref name="targetType"/> is possible; Otherwise, <see langword="false"/>.</returns>
    /// <remarks>This method has a side effect and saves the new instance of <paramref name="targetType"/> in thread-local storage if successful.</remarks>
    [RequireNotNull(nameof(targetType))]
    private static bool CanCreateInstanceOfVerified(this Type targetType)
    {
        FieldInfo[] Fields = targetType.GetFields();
        FieldInfo? StaticFieldInfo = Fields.FirstOrDefault(IsStaticInstance);

        ConstructorInfo[] Constructors = targetType.GetConstructors();
        ConstructorInfo? ParameterlessConstructorInfo = Constructors.FirstOrDefault((ConstructorInfo constructor) => constructor.GetParameters().Length == 0);

        object? NullableInstance = null;

        if (StaticFieldInfo is not null)
            NullableInstance = StaticFieldInfo.GetValue(null);

        if (ParameterlessConstructorInfo is not null)
            NullableInstance = ParameterlessConstructorInfo.Invoke([]);

        if (IsNullableValueType(targetType, out Type ValueType))
        {
            ConstructorInfo[] ValidConstructors = Contract.AssertNotNull(Constructors);
            Contract.Require(ValidConstructors.Length == 1);

            ConstructorInfo ValueConstructorInfo = ValidConstructors[0];
            ParameterInfo[] Parameters = ValueConstructorInfo.GetParameters();
            Contract.Require(Parameters.Length == 1);

            ParameterInfo ValueParameter = Parameters[0];
            Contract.Require(ValueParameter.ParameterType == ValueType);

            object CreateInstanceOfValueType()
            {
                return Contract.AssertNotNull(Activator.CreateInstance(ValueType));
            }

            object DefaultValueParameter = Contract.AssertNotNull(Contract.AssertNoThrow(CreateInstanceOfValueType));
            NullableInstance = ValueConstructorInfo.Invoke([DefaultValueParameter]);
        }

        if (NullableInstance is null)
        {
            return false;
        }
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
                Contract.Assert(GenericArguments.Length == 1);

                Type FirstGenericArgument = GenericArguments[0];
                Contract.Assert(FirstGenericArgument.IsValueType);

                valueType = FirstGenericArgument;
                return true;
            }
        }

        Contract.Unused(out valueType);
        return false;
    }

    private static bool IsStaticInstance(FieldInfo field)
    {
        FieldAttributes Attributes = field.Attributes;

        return Attributes.HasFlag(FieldAttributes.Static) && Attributes.HasFlag(FieldAttributes.InitOnly);
    }

    /// <summary>
    /// Gets the object instance created by <see cref="CanCreateInstanceOf(Type)"/>.
    /// </summary>
    /// <returns>The object instance created by <see cref="CanCreateInstanceOf(Type)"/>.</returns>
    /// <remarks>Reading this property is valid if and only if <see cref="CanCreateInstanceOf(Type)"/> returned true.</remarks>
    public static object LastInstance
    {
        get => Contract.AssertNotNull(LastInstanceInternal.Value);
        private set => LastInstanceInternal.Value = value;
    }

    private static readonly ThreadLocal<object> LastInstanceInternal = new();
}
