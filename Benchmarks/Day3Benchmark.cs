using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

namespace AdventOfCode2025.Benchmarks;

[InvocationCount(1, 1)]
public class Day3Benchmark : IBenchmarkedSolution
{
    private string[] _banks = [];
    [Benchmark, IterationSetup(Targets = [nameof(Part1), nameof(Part2)])]
    public void Setup()
    {
        ManifestEmbeddedFileProvider files = new(typeof(IBenchmarkedSolution).Assembly, "Assets/Data/Day3");
        _banks = files.GetFileInfo("input.txt").CreateReadStream().ReadLines();
    }
    [Benchmark]
    public object Part1()
    {
        long total = 0;
        foreach (string bank in _banks)
        {
            long largest = Solutions.Day3.GetLargest(bank, 2);
            total += largest;
        }
        return total;
    }
    [Benchmark]
    public object Part2()
    {
        long total = 0;
        foreach (string bank in _banks)
        {
            long largest = Solutions.Day3.GetLargest(bank, 12);
            total += largest;
        }
        return total;
    }
}
