using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Running;

using Spectre.Console;

namespace AdventOfCode2025.Benchmarks;

internal class BenchmarkProgram
{
    internal static void Run(string[] args)
    {
        int day = 0;
        // Parse arguments
        AnsiConsole.MarkupLine("[cyan]Choose the day to benchmark:[/]");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out day) && day > 0 && day <= 12)
                break;
            AnsiConsole.MarkupLine("[red]Invalid input. Please enter a valid day number.[/]");
        }
        try
        {
            Type? dayBenchmark = Type.GetType($"AdventOfCode2025.Benchmarks.Day{day}Benchmark");
            if (dayBenchmark == null)
            {
                AnsiConsole.MarkupLine($"[red]Benchmark for Day {day} not found.[/]");
                return;
            }
            BenchmarkRunner.Run(dayBenchmark);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error loading benchmark: {ex.Message}[/]");
            return;
        }
        AnsiConsole.WriteLine("Press any key to exit.");
    }
}
