namespace XamlConverterLibrary.Test;

internal class NullToObjectConverterTestClass
{
    public string? StringPropertyNull { get; set; }
    public string? StringPropertyNotNull { get; set; } = string.Empty;
    public int? IntPropertyNull { get; set; }
    public int? IntPropertyNotNull { get; set; } = 0;
}
