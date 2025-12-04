using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal class Day4 : ISolution
{
    public int Day { get; } = 4;
    public string Name { get; } = "Printing Department";
    private char[][] _grid;
    private bool _isTesting = false;

    public async Task Setup(ManifestEmbeddedFileProvider files, bool isTesting)
    {
        _isTesting = isTesting;
        var lines = files.GetFileInfo(isTesting ? "test.txt" : "input.txt").CreateReadStream().ReadLines();
        _grid = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            _grid[i] = new char[line.Length];
            for (int j = 0; j < line.Length; j++)
            {
                _grid[i][j] = line[j];
            }
        }
    }

    public async Task SolvePart1()
    {
        int total = 0;
        for (int y = 0; y < _grid.Length; y++)
            for(int x = 0; x < _grid[y].Length; x++)
                if(_grid[y][x] == '@' && CheckSurroundings(_grid, y, x) < 4)
                {
                    AnsiConsole.MarkupLine($"[yellow]Found accessible roll at ({y},{x})[/]");
                    total++;
                }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total number of paper rolls with less than 4 surrounding rolls is {total}.");
    }

    public async Task SolvePart2()
    {
        int total = 0;
        int pass = 1;
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            if(_isTesting)
                AnsiConsole.MarkupLine($"[blue]--- Pass {pass} ---[/]");
            for (int y = 0; y < _grid.Length; y++)
                for (int x = 0; x < _grid[y].Length; x++)
                    if (_grid[y][x] == '@' && CheckSurroundings(_grid, y, x) < 4)
                    {
                        if(_isTesting)
                            AnsiConsole.MarkupLine($"[yellow]Removing accessible roll at at ({y},{x})[/]");
                        _grid[y][x] = '.';
                        isDone = false;
                        total++;
                    }
            pass++;
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total number of paper rolls that can be removed is {total}.");
    }

    public static int CheckSurroundings(char[][] grid, int x, int y, char targetChar = '@')
    {
        int count = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                int newX = x + i;
                int newY = y + j;
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
                {
                    if (grid[newX][newY] == targetChar)
                        count++;
                }
            }
        return count;
    }
}
