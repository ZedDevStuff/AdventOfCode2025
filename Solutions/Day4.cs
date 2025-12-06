using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal class Day4 : IDay
{
    public int Day { get; } = 4;
    public string Name { get; } = "Printing Department";
    public string Part1Template { get; } = "[green]Answer:[/] The total number of paper rolls with less than 4 surrounding rolls is {0}.";
    public string Part2Template { get; } = "[green]Answer:[/] The total number of paper rolls that can be removed is {0}.";

    public object ParsePart1(string input)
    {
        char[][] grid = [];
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        grid = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            grid[i] = new char[line.Length];
            for (int j = 0; j < line.Length; j++)
            {
                grid[i][j] = line[j];
            }
        }
        return grid;
    }
    public object ParsePart2(string input) => ParsePart1(input);

    public object SolvePart1(object input)
    {
        char[][] grid = (char[][])input;
        int total = 0;
        for (int y = 0; y < grid.Length; y++)
            for(int x = 0; x < grid[y].Length; x++)
                if(grid[y][x] == '@' && CheckSurroundings(grid, y, x) < 4)
                {
#if DEBUG
                    AnsiConsole.MarkupLine($"[yellow]Found accessible roll at ({y},{x})[/]");
#endif
                    total++;
                }
        return total;
    }

    public object SolvePart2(object input)
    {
        char[][] grid = (char[][])input;
        int total = 0;
        int pass = 1;
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
#if DEBUG
            AnsiConsole.MarkupLine($"[blue]--- Pass {pass} ---[/]");
#endif
            for (int y = 0; y < grid.Length; y++)
                for (int x = 0; x < grid[y].Length; x++)
                    if (grid[y][x] == '@' && CheckSurroundings(grid, y, x) < 4)
                    {
#if DEBUG
                        AnsiConsole.MarkupLine($"[yellow]Removing accessible roll at at ({y},{x})[/]");
#endif
                        grid[y][x] = '.';
                        isDone = false;
                        total++;
                    }
            pass++;
        }
        return total;
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
