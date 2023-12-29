namespace XamlConverterLibrary.Test;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes
internal class StaticMutableFieldTestClass
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
{
#pragma warning disable CS0169 // The field 'PrivateStaticTestClass.PrivateStaticField' is never used
#pragma warning disable CS0649 // Field 'PrivateStaticTestClass.PrivateStaticField' is never assigned to, and will always have its default value null
    public static string? StaticMutableField;
#pragma warning restore CS0649 // Field 'PrivateStaticTestClass.PrivateStaticField' is never assigned to, and will always have its default value null
#pragma warning restore CS0169 // The field 'PrivateStaticTestClass.PrivateStaticField' is never used

    public StaticMutableFieldTestClass(string unused1, string unused2)
    {
    }

    public static void SetMutableField()
    {
        StaticMutableField = string.Empty;
    }
}
