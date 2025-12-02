using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal partial class Day2 : ISolution
{
    public int Day { get; } = 2;
    public string Name { get; } = "Gift Shop";
    private bool _isTesting = false;
    private Range[] _ranges = [];
    public async Task Setup(ManifestEmbeddedFileProvider files, bool isTesting)
    {
        _isTesting = isTesting;
        string[] ranges = files.GetFileInfo(_isTesting ? "test.txt" : "input.txt").CreateReadStream().ReadToEnd().Split(',');
        List<Range> tempRanges = [];
        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            tempRanges.Add(new (long.Parse(parts[0]), long.Parse(parts[1])));
        }
        _ranges = [.. tempRanges];
    }
    // I have been enlightened by the power of regex. My previous solution in *theory* did the same thing
    // but it was so overengineered that i didn't even know why it didn't work on my actual input.
    [GeneratedRegex(@"^(\d+)\1$", RegexOptions.Compiled)]
    private static partial Regex RepeatRegex();
    [GeneratedRegex(@"^(\d+)\1+$", RegexOptions.Compiled)]
    private static partial Regex RepeatMultipleRegex();
    public async Task SolvePart1()
    {
        long total = 0;
        foreach (var range in _ranges)
            for(long i = range.Start;i <= range.End;i++)
                if(RepeatRegex().IsMatch(i.ToString()))
                    total += i;
        AnsiConsole.MarkupLine($"[green]Answer:[/] The sum of all invalid IDs is: {total}");
    }

    public async Task SolvePart2()
    {
        long total = 0;
        foreach (var range in _ranges)
            for (long i = range.Start; i <= range.End; i++)
                if (RepeatMultipleRegex().IsMatch(i.ToString()))
                    total += i;
        AnsiConsole.MarkupLine($"[green]Answer:[/] The sum of all invalid IDs is: {total}");
    }

    public struct Range(long start, long end)
    {
        public long Start = start;
        public long End = end;

        public readonly bool IsWithinInclusive(long number)
        {
            return number <= End && number >= Start;
        }
        public readonly bool IsWithinExclusive(long number)
        {
            return number < End && number > Start;
        }
    }
}
