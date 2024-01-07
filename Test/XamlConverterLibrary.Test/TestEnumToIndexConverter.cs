namespace XamlConverterLibrary.Test;

using System.Threading;
using NUnit.Framework;

[TestFixture]
public class TestEnumToIndexConverter
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvert()
    {
        EnumToIndexConverterTestConvertWindow Dlg = new();
        Dlg.Show();

        Assert.That(Dlg.EnumPropertyIndexZero.SelectedIndex, Is.EqualTo(0));
        Assert.That(Dlg.EnumPropertyIndexOne.SelectedIndex, Is.EqualTo(1));
        Assert.That(Dlg.EnumPropertyIndexTwo.SelectedIndex, Is.EqualTo(2));
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertBack()
    {
        EnumToIndexConverterTestConvertBackWindow Dlg = new();
        Dlg.Show();
        EnumToIndexConverterTestClass DataContext = (EnumToIndexConverterTestClass)Dlg.DataContext;

        Assert.That(DataContext.EnumPropertyIndexZero, Is.EqualTo(EnumToIndexConverterTestEnum.IndexOne));
        Assert.That(DataContext.EnumPropertyIndexOne, Is.EqualTo(EnumToIndexConverterTestEnum.IndexTwo));
        Assert.That(DataContext.EnumPropertyIndexTwo, Is.EqualTo(EnumToIndexConverterTestEnum.IndexZero));
    }
}
