using System;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal class Day3 : ISolution
{
    public int Day { get; } = 3;
    public string Name { get; } = "Lobby";
    private bool _isTesting = false;
    private string[] _banks = [];

    public async Task Setup(ManifestEmbeddedFileProvider files, bool isTesting)
    {
        _isTesting = isTesting;
        _banks = files.GetFileInfo(_isTesting ? "test.txt" : "input.txt").CreateReadStream().ReadLines();
    }
    public async Task SolvePart1()
    {
        long total = 0;
        foreach(string bank in _banks)
        {
            long largest = GetLargest(bank, 2);
            if (_isTesting)
                AnsiConsole.MarkupLine($"[yellow]Found {largest} in bank '{bank}'[/]");
            total += largest;
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total output joltage is: {total}");
    }
    public async Task SolvePart2()
    {
        long total = 0;
        foreach (string bank in _banks)
        {
            long largest = GetLargest(bank, 12);
            if(_isTesting)
                AnsiConsole.MarkupLine($"[yellow]Found {largest} in bank '{bank}'[/]");
            total += largest;
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total output joltage is: {total}");
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
