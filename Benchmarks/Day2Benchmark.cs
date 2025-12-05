using System.Collections.Generic;

using AdventOfCode2025.Solutions.Misc;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

namespace AdventOfCode2025.Benchmarks;

[InvocationCount(1, 1)]
public class Day2Benchmark : IBenchmarkedSolution
{
    private Range[] _ranges = [];
    [Benchmark, IterationSetup(Targets = [nameof(Part1), nameof(Part2)])]
    public void Setup()
    {
        ManifestEmbeddedFileProvider files = new(typeof(IBenchmarkedSolution).Assembly, "Assets/Data/Day2");
        string[] ranges = files.GetFileInfo("input.txt").CreateReadStream().ReadToEnd().Split(',');
        List<Range> tempRanges = [];
        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            tempRanges.Add(new Range(long.Parse(parts[0]), long.Parse(parts[1])));
        }
        _ranges = [.. tempRanges];
    }
    [Benchmark]
    public object Part1()
    {
        long total = 0;
        foreach (var range in _ranges)
            for (long i = range.Start; i <= range.End; i++)
                if (Solutions.Day2.RepeatRegex().IsMatch(i.ToString()))
                    total += i;
        return total;
    }
    [Benchmark]
    public object Part2()
    {
        long total = 0;
        foreach (var range in _ranges)
            for (long i = range.Start; i <= range.End; i++)
                if (Solutions.Day2.RepeatMultipleRegex().IsMatch(i.ToString()))
                    total += i;
        return total;
    }
}
