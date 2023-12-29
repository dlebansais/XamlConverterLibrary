namespace XamlConverterLibrary.Test;

using System;
using System.Collections.Generic;
using Converters;
using NUnit.Framework;

[TestFixture]
public class TestConverterTools
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
        Assert.That(typeof(NonStaticFieldTestClass).CanCreateInstanceOf(), Is.False);
        Assert.That(typeof(StaticMutableFieldTestClass).CanCreateInstanceOf(), Is.False);
    }

    [Test]
    public void TestLastInstance()
    {
        Assert.That(typeof(string).CanCreateInstanceOf(), Is.True);
        Assert.That(ConverterTools.LastInstance, Is.EqualTo(string.Empty));
    }
}
