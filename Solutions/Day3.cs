using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            int[] indices = [-1, -1];
            for(int i = 57;i > 48;i--)
            {
                int index = bank.IndexOf((char)i, indices[0] == -1 ? 0 : indices[0] + 1);
                if (index != -1)
                {
                    if (indices[0] == -1)
                    {
                        if (index == bank.Length - 1 && indices[0] == -1)
                        {
                            indices[1] = index;
                            continue;
                        }
                        else
                        {
                            i++;
                            indices[0] = index;
                            continue;
                        }
                    }
                    else if (indices[1] == -1)
                    {
                        indices[1] = index;
                        break;
                    }
                    else break;
                }
            }
            if (indices[0] < indices[1])
            {
                AnsiConsole.MarkupLine($"[yellow]Found {bank[indices[0]]}{bank[indices[1]]} in bank '{bank}'[/]");
                total += long.Parse($"{bank[indices[0]]}{bank[indices[1]]}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]Found {bank[indices[1]]}{bank[indices[0]]} in bank '{bank}'[/]");
                total += long.Parse($"{bank[indices[1]]}{bank[indices[0]]}");
            }
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total output joltage is: {total}");
    }
    // I was not ready for this
    public async Task SolvePart2()
    {

    }
}
