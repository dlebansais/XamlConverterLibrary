namespace XamlConverterLibrary.Test;

using System.Threading;
using NUnit.Framework;

[TestFixture]
internal class TestBooleanToObjectConverter
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvert()
    {
        BooleanToObjectConverterTestConvertWindow Dlg = new();
        Dlg.Show();

        Assert.That(Dlg.BoolPropertyTrue.IsVisible, Is.True);
        Assert.That(Dlg.BoolPropertyFalse.IsVisible, Is.False);
        Assert.That(Dlg.NullableBoolProperty3.IsVisible, Is.True);
        Assert.That(Dlg.NullableBoolProperty3True.IsVisible, Is.True);
        Assert.That(Dlg.NullableBoolProperty3False.IsVisible, Is.False);
        Assert.That(Dlg.NullableBoolProperty2.IsVisible, Is.False);
        Assert.That(Dlg.NullableBoolProperty2True.IsVisible, Is.True);
        Assert.That(Dlg.NullableBoolProperty2False.IsVisible, Is.False);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertBack()
    {
        BooleanToObjectConverterTestConvertBackWindow Dlg = new();
        Dlg.Show();
        BooleanToObjectConverterTestClass DataContext = (BooleanToObjectConverterTestClass)Dlg.DataContext;

        Assert.That(DataContext.BoolPropertyTrue, Is.False);
        Assert.That(DataContext.BoolPropertyFalse, Is.True);
        Assert.That(DataContext.NullableBoolProperty, Is.True);
        Assert.That(DataContext.NullableBoolPropertyTrue, Is.Null);
        Assert.That(DataContext.NullableBoolPropertyFalse, Is.True);
    }
}
