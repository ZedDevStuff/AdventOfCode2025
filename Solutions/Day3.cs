using System;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal class Day3 : IDay
{
    public int Day { get; } = 3;
    public string Name { get; } = "Lobby";
    public string Part1Template { get; } = "[green]Answer:[/] The total output joltage is: {0}";
    public string Part2Template { get; } = "[green]Answer:[/] The total output joltage is: {0}";

    public object ParsePart1(string input)
    {
        return input.Split(Environment.NewLine);
    }
    public object ParsePart2(string input) => input.Split(Environment.NewLine);
    public object SolvePart1(object input)
    {
        string[] banks = (string[])input;
        long total = 0;
        foreach(string bank in banks)
        {
            long largest = GetLargest(bank, 2);
#if DEBUG
            AnsiConsole.MarkupLine($"[yellow]Found {largest} in bank '{bank}'[/]");
#endif
            total += largest;
        }
        return total;
    }
    public object SolvePart2(object input)
    {
        string[] banks = (string[])input;
        long total = 0;
        foreach (string bank in banks)
        {
            long largest = GetLargest(bank, 12);
#if DEBUG
            AnsiConsole.MarkupLine($"[yellow]Found {largest} in bank '{bank}'[/]");
#endif
            total += largest;
        }
        return total;
    }
    // I think i actually loved this one
    public static long GetLargest(string bank, int length)
    {
        int[] indices = new int[length];
        int startIndex = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 57; j > 48; j--)
            {
                int index = bank.IndexOf((char)j, startIndex);
                if (i == 0 && index != -1)
                {
                    if (bank.Length - index < length)
                        continue;
                }
                if (index != -1 && bank.Length - index >= length - i)
                {
                    indices[i] = index;
                    startIndex = index + 1;
                    break;
                }
            }
        }
        return long.Parse(string.Concat(Array.ConvertAll(indices, i => bank[i])));
    }
}
