using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using AdventOfCode2025.Solutions;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

namespace AdventOfCode2025.Benchmarks;

public abstract class BaseBenchmark
{
    public abstract int Day { get; }
    public IDay Solution { get; internal set; } = null!;
    public string Input { get; internal set; } = string.Empty;
    public object ParsedPart1 { get; internal set; }
    public object ParsedPart2 { get; internal set; }
    [GlobalSetup]
    public void Setup()
    {
        var files = new ManifestEmbeddedFileProvider(typeof(BaseBenchmark).Assembly, $"Assets/Data/Day{Day}");
        Input = files.GetFileInfo("input.txt").CreateReadStream().ReadToEnd();
        Type solutionType = Type.GetType($"AdventOfCode2025.Solutions.Day{Day}") ?? throw new InvalidOperationException($"Solution for Day {Day} not found.");
        Solution = (IDay?)Activator.CreateInstance(solutionType);
        ParsedPart1 = ParsePart1();
        ParsedPart2 = ParsePart2();
    }
    public abstract object ParsePart1();
    public abstract object SolvePart1();
    public abstract object ParsePart2();
    public abstract object SolvePart2();
}
