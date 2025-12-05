using System.Linq;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

using Range = AdventOfCode2025.Solutions.Misc.Range;

namespace AdventOfCode2025.Benchmarks;

[InvocationCount(1, 1)]
public class Day5Benchmark
{
    private Solutions.Day5.RangeCollection _ranges = null!;
    private long[] _products = null!;

    [Benchmark, IterationSetup(Targets = [nameof(Part1), nameof(Part2)])]
    public void Setup()
    {
        ManifestEmbeddedFileProvider files = new(typeof(IBenchmarkedSolution).Assembly, "Assets/Data/Day5");
        string[] sections = files.GetFileInfo("input.txt").CreateReadStream().ReadToEnd().Split("\r\n\r\n");
        Range[] ranges = sections[0].Split("\r\n").Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(r =>
            {
                string[] split = r.Split('-');
                return new Range(long.Parse(split[0]), long.Parse(split[1]));
            }).ToArray();
        _ranges = new(ranges);
        _products = sections[1].Split('\n')
            .Select(long.Parse)
            .ToArray();
    }
    [Benchmark]
    public object Part1()
    {
        long total = 0;
        foreach (var product in _products)
        {
            if (_ranges.IsInAnyInclusive(product))
                total++;
        }
        return total;
    }
    [Benchmark]
    public object Part2()
    {
        long total = 0;
        _ranges.MergeOverlapping();
        total = _ranges.Ranges
            .Select(r => r.Length + 1)
            .Sum();
        return total;
    }
}
