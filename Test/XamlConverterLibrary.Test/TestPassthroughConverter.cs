namespace XamlConverterLibrary.Test;

using System.Threading;
using NUnit.Framework;

[TestFixture]
internal class TestPassthroughConverter
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvert()
    {
        PassthroughConverterTestConvertWindow Dlg = new();
        Dlg.Show();

        Assert.That(Dlg.StringProperty.Text, Is.EqualTo("Test"));
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertBack()
    {
        PassthroughConverterTestConvertBackWindow Dlg = new();
        Dlg.Show();
        PassthroughConverterTestClass DataContext = (PassthroughConverterTestClass)Dlg.DataContext;

        Assert.That(DataContext.StringProperty, Is.EqualTo("Test"));
    }
}
