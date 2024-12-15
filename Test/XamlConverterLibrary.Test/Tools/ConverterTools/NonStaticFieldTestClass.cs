namespace XamlConverterLibrary.Test;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes
internal class NonStaticFieldTestClass
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
{
#pragma warning disable CS0169 // The field 'PrivateStaticTestClass.PrivateStaticField' is never used
#pragma warning disable CS0649 // Field 'PrivateStaticTestClass.PrivateStaticField' is never assigned to, and will always have its default value null
    public readonly string? NonStaticField;
#pragma warning restore CS0649 // Field 'PrivateStaticTestClass.PrivateStaticField' is never assigned to, and will always have its default value null
#pragma warning restore CS0169 // The field 'PrivateStaticTestClass.PrivateStaticField' is never used

#pragma warning disable IDE0060 // Remove unused parameter
    public NonStaticFieldTestClass(string unused1, string unused2)
#pragma warning restore IDE0060 // Remove unused parameter
    {
    }
}
