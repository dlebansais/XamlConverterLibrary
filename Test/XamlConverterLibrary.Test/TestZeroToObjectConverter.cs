namespace XamlConverterLibrary.Test;

using System.Threading;
using NUnit.Framework;

[TestFixture]
public class TestZeroToObjectConverter
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvert()
    {
        ZeroToObjectConverterTestConvertWindow Dlg = new();
        Dlg.Show();

        Assert.That(Dlg.IntPropertyOne.IsVisible, Is.True);
        Assert.That(Dlg.IntPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableIntProperty3.IsVisible, Is.True);
        Assert.That(Dlg.NullableIntProperty3One.IsVisible, Is.True);
        Assert.That(Dlg.NullableIntProperty3Zero.IsVisible, Is.False);
        Assert.That(Dlg.NullableIntProperty2.IsVisible, Is.False);
        Assert.That(Dlg.NullableIntProperty2One.IsVisible, Is.True);
        Assert.That(Dlg.NullableIntProperty2Zero.IsVisible, Is.False);

        Assert.That(Dlg.BytePropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.SBytePropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.ShortPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.UShortPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.UIntPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.LongPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.ULongPropertyZero.IsVisible, Is.False);

        Assert.That(Dlg.NullableBytePropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableSBytePropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableShortPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableUShortPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableUIntPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableLongPropertyZero.IsVisible, Is.False);
        Assert.That(Dlg.NullableULongPropertyZero.IsVisible, Is.False);

        Assert.That(Dlg.ZeroLengthStringProperty.IsVisible, Is.False);
        Assert.That(Dlg.NonZeroLengthStringProperty.IsVisible, Is.True);
        Assert.That(Dlg.EnumProperty.IsVisible, Is.False);
        Assert.That(Dlg.ZeroCountListProperty.IsVisible, Is.False);
        Assert.That(Dlg.NonZeroCountListProperty.IsVisible, Is.True);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertBack()
    {
        ZeroToObjectConverterTestConvertBackWindow Dlg = new();
        Dlg.Show();
        ZeroToObjectConverterTestClass DataContext = (ZeroToObjectConverterTestClass)Dlg.DataContext;

        Assert.That(DataContext.IntPropertyOne, Is.Zero);
        Assert.That(DataContext.IntPropertyZero, Is.EqualTo(1));
        Assert.That(DataContext.NullableIntProperty, Is.EqualTo(1));
        Assert.That(DataContext.NullableIntPropertyOne, Is.Null);
        Assert.That(DataContext.NullableIntPropertyZero, Is.EqualTo(1));
    }
}
