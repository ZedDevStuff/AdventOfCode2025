using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

namespace AdventOfCode2025.Benchmarks;

public class Day1Benchmark : IBenchmarkedSolution
{
    private string[] _lines = [];
    [Benchmark, IterationSetup(Targets = [nameof(Part1), nameof(Part2)])]
    public void Setup()
    {
        ManifestEmbeddedFileProvider files = new (typeof(Day1Benchmark).Assembly, "Assets/Data/Day1");
        _lines = files.GetFileInfo("input.txt").CreateReadStream().ReadLines();
    }
    [Benchmark]
    public object Part1()
    {
        int currentNumber = 50;
        int zerosCount = 0;
        foreach (string line in _lines)
        {
            int value = int.Parse(line[1..]);
            if (line[0] == 'L')
                value = -value;
            currentNumber = Solutions.Day1.Wrap(currentNumber + value, 100);
            if (currentNumber == 0)
                zerosCount++;
        }
        return zerosCount;
    }
    [Benchmark]
    public object Part2()
    {
        int currentNumber = 50;
        int zerosCount = 0;
        foreach (string line in _lines)
        {
            int value = int.Parse(line[1..]);
            if (line[0] == 'L')
                value = -value;
            currentNumber = Solutions.Day1.BruteForceWrap(currentNumber, value, 0, 99, out int zeros);
            zerosCount += zeros;
            if (currentNumber == 0)
                zerosCount++;
        }
        return zerosCount;
    }
}
