namespace XamlConverterLibrary.Test;

using System;
using System.Globalization;
using System.Threading;
using Converters;
using NUnit.Framework;

[TestFixture]
internal class TestOneWayOnlyConverter
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvert()
    {
        OneWayOnlyConverterTestConvertWindow Dlg = new();
        Dlg.Show();

        Assert.That(Dlg.StringProperty.Text, Is.EqualTo("Test"));
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertNull()
    {
        SampleOneWayOnlyConverter Converter = new();

        _ = Assert.Throws<ArgumentNullException>(() => Converter.Convert(null, GetType(), new object(), CultureInfo.InvariantCulture));
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestConvertBack()
    {
        SampleOneWayOnlyConverter Converter = new();

        _ = Assert.Throws<NotSupportedException>(() => Converter.ConvertBack(new object(), GetType(), new object(), CultureInfo.InvariantCulture));
    }
}
