using BenchmarkDotNet.Attributes;

namespace AdventOfCode2025.Benchmarks;

public interface IBenchmarkedSolution
{
    [Benchmark, IterationSetup(Targets = [nameof(Part1), nameof(Part2)])]
    public void Setup();
    [Benchmark]
    public object Part1();
    [Benchmark]
    public object Part2();
}
