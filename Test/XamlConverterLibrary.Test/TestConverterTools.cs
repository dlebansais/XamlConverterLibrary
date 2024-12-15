namespace XamlConverterLibrary.Test;

using System;
using System.Collections.Generic;
using Converters;
using NUnit.Framework;

[TestFixture]
internal class TestConverterTools
{
    [Test]
    public void TestIsNullable()
    {
        Assert.That(typeof(string).IsNullable(), Is.True);
        Assert.That(typeof(int?).IsNullable(), Is.True);
        Assert.That(typeof(int).IsNullable(), Is.False);
        Assert.That(typeof(List<int>).IsNullable(), Is.True);
    }

    [Test]
    public void TestCanCreateInstanceOf()
    {
        Assert.That(typeof(string).CanCreateInstanceOf(), Is.True);
        Assert.That(typeof(int?).CanCreateInstanceOf(), Is.True);
        Assert.That(typeof(int).CanCreateInstanceOf(), Is.False);
        Assert.That(typeof(List<int>).CanCreateInstanceOf(), Is.True);

        NonStaticFieldTestClass TestIntance0 = new(string.Empty, string.Empty);
        Assert.That(TestIntance0.GetType().CanCreateInstanceOf(), Is.False);

        StaticMutableFieldTestClass.SetMutableField();
        StaticMutableFieldTestClass TestIntance1 = new(string.Empty, string.Empty);
        Assert.That(TestIntance1.GetType().CanCreateInstanceOf(), Is.False);
    }

    [Test]
    public void TestLastInstance()
    {
        Assert.That(typeof(string).CanCreateInstanceOf(), Is.True);
        Assert.That(ConverterTools.LastInstance, Is.EqualTo(string.Empty));
    }
}
