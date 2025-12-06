using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

public interface IDay
{
    public int Day { get; }
    public string Name { get; }
    public string FullName => $"Day {Day}: {Name}";
    public string Part1Template { get; }
    public string Part2Template { get; }

    public object ParsePart1(string input);
    public object ParsePart2(string input);
    public object SolvePart1(object input);
    public object SolvePart2(object input);
    public void PrintPart1Answer(string inputString)
    {
        AnsiConsole.MarkupLine($"[yellow]--- Day {Day}: {Name} ---[/]");
        var input = ParsePart1(inputString);
        var answer = SolvePart1(input);
        AnsiConsole.MarkupLine(Part1Template, answer);
    }
    public void PrintPart2Answer(string inputString)
    {
        AnsiConsole.MarkupLine($"[yellow]--- Day {Day}: {Name} ---[/]");
        var input = ParsePart2(inputString);
        var answer = SolvePart2(input);
        AnsiConsole.MarkupLine(Part2Template, answer);
    }
}
