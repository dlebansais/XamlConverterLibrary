namespace XamlConverterLibrary.Test;

using System.Threading;
using NUnit.Framework;

[TestFixture]
public class TestNullToObjectConverter
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvert()
    {
        NullToObjectConverterTestConvertWindow Dlg = new();
        Dlg.Show();

        Assert.That(Dlg.StringPropertyNull.IsVisible, Is.False);
        Assert.That(Dlg.StringPropertyNotNull.IsVisible, Is.True);
        Assert.That(Dlg.IntPropertyNull.IsVisible, Is.False);
        Assert.That(Dlg.IntPropertyNotNull.IsVisible, Is.True);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertBack()
    {
        NullToObjectConverterTestConvertBackWindow Dlg = new();
        Dlg.Show();
        NullToObjectConverterTestClass DataContext = (NullToObjectConverterTestClass)Dlg.DataContext;

        Assert.That(DataContext.StringPropertyNull, Is.Not.Null);
        Assert.That(DataContext.StringPropertyNotNull, Is.Null);
        Assert.That(DataContext.IntPropertyNull, Is.Not.Null);
        Assert.That(DataContext.IntPropertyNotNull, Is.Null);
    }
}
