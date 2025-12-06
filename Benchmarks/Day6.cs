using BenchmarkDotNet.Attributes;

namespace AdventOfCode2025.Benchmarks;

public class Day6 : BaseBenchmark
{
    public override int Day => 6;

    [Benchmark]
    public override object ParsePart1() => Solution.ParsePart1(Input);
    [Benchmark]
    public override object ParsePart2() => Solution.ParsePart2(Input);
    [Benchmark]
    public override object SolvePart1() => Solution.SolvePart1(ParsedPart1);

    [Benchmark]
    public override object SolvePart2() => Solution.SolvePart2(ParsedPart2);
}
