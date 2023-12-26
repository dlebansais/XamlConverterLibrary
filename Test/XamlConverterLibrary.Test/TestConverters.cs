namespace XamlConverterLibrary.Test;

using System;
using System.Threading;
using NUnit.Framework;

[TestFixture]
public class TestConverters
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestSuccess()
    {
        TestWindow Dlg = new();
        Dlg.Show();
    }
}
