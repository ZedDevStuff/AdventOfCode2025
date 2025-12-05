using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AdventOfCode2025.Solutions.Misc;

public struct Range
{
    public long Start;
    public long End;
    public long Length => End - Start;

    public Range(long start, long end)
    {
        if(start > end)
            throw new ArgumentException("Start of range cannot be greater than end.");
        Start = start;
        End = end;
    }

    public readonly bool IsWithinInclusive(long number)
    {
        return number <= End && number >= Start;
    }
    public readonly bool IsWithinExclusive(long number)
    {
        return number < End && number > Start;
    }

    public readonly bool Overlaps(Range other)
    {
        return Start <= other.End && End >= other.Start;
    }
    public Range Merge(Range other)
    {
        if (!Overlaps(other))
            throw new InvalidOperationException("Ranges do not overlap and cannot be merged.");
        return new Range(Math.Min(Start, other.Start), Math.Max(End, other.End));
    }
    private Range MergeNoCheck(Range other)
    {
        return new Range(Math.Min(Start, other.Start), Math.Max(End, other.End));
    }
    public bool TryMerge(Range other, out Range merged)
    {
        if (Overlaps(other))
        {
            merged = MergeNoCheck(other);
            return true;
        }
        merged = this;
        return false;
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Range other && Start == other.Start && End == other.End;
    }
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
    public override readonly string ToString()
    {
        return $"[{Start}, {End}]";
    }

    public static bool operator ==(Range left, Range right) => left.Equals(right);
    public static bool operator !=(Range left, Range right) => !left.Equals(right);
}
