namespace XamlConverterLibrary.Test;

internal class BooleanToObjectConverterTestClass
{
    public bool BoolPropertyTrue { get; set; } = true;
    public bool BoolPropertyFalse { get; set; }
    public bool? NullableBoolProperty { get; set; }
    public bool? NullableBoolPropertyTrue { get; set; } = true;
    public bool? NullableBoolPropertyFalse { get; set; } = false;
}
