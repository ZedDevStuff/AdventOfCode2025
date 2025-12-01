using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025;

public static class StreamExtensions
{
    public static string ReadToEnd(this System.IO.Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false)
    {
        using var reader = new System.IO.StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        return reader.ReadToEnd();
    }
    public static string? ReadLine(this System.IO.Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false)
    {
        using var reader = new System.IO.StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        return reader.ReadLine() ?? string.Empty;
    }
    public static string[] ReadLines(this System.IO.Stream stream, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = -1, bool leaveOpen = false)
    {
        return stream.ReadToEnd(encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen).Split(["\r\n", "\n"], StringSplitOptions.None);
    }
}
