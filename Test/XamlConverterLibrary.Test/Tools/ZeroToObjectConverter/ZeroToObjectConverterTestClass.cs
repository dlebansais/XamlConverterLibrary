namespace XamlConverterLibrary.Test;

using System.Collections.Generic;
using NUnit.Framework;

internal class ZeroToObjectConverterTestClass
{
    public int IntPropertyOne { get; set; } = 1;
    public int IntPropertyZero { get; set; }
    public int? NullableIntProperty { get; set; }
    public int? NullableIntPropertyOne { get; set; } = 1;
    public int? NullableIntPropertyZero { get; set; } = 0;

    public byte BytePropertyZero { get; }
    public sbyte SBytePropertyZero { get; }
    public short ShortPropertyZero { get; }
    public ushort UShortPropertyZero { get; }
    public uint UIntPropertyZero { get; }
    public long LongPropertyZero { get; }
    public ulong ULongPropertyZero { get; }

    public byte? NullableBytePropertyZero { get; }
    public sbyte? NullableSBytePropertyZero { get; }
    public short? NullableShortPropertyZero { get; }
    public ushort? NullableUShortPropertyZero { get; }
    public uint? NullableUIntPropertyZero { get; }
    public long? NullableLongPropertyZero { get; }
    public ulong? NullableULongPropertyZero { get; }

    public string ZeroLengthStringProperty { get; } = string.Empty;
    public string NonZeroLengthStringProperty { get; } = "Not zero";
    public ActionTargets EnumProperty { get; } = ActionTargets.Default;
    public List<string> ZeroCountListProperty { get; } = new();
    public List<string> NonZeroCountListProperty { get; } = new() { string.Empty };
}
