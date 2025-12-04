using System;
using System.Collections.Generic;
using System.Text;

using BenchmarkDotNet.Attributes;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Benchmarks;

public class Day4Benchmark : IBenchmarkedSolution
{
    private char[][] _grid;
    [Benchmark, IterationSetup(Targets = [nameof(Part1), nameof(Part2)])]
    public void Setup()
    {
        ManifestEmbeddedFileProvider files = new(typeof(IBenchmarkedSolution).Assembly, "Assets/Data/Day4");
        var lines = files.GetFileInfo("input.txt").CreateReadStream().ReadLines();
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
    [Benchmark]
    public object Part1()
    {
        int total = 0;
        for (int y = 0; y < _grid.Length; y++)
        {
            for (int x = 0; x < _grid[y].Length; x++)
            {
                if (_grid[y][x] == '@' && Solutions.Day4.CheckSurroundings(_grid, y, x) < 4)
                {
                    AnsiConsole.MarkupLine($"[yellow]Found accessible roll at ({y},{x})[/]");
                    total++;
                }
            }
        }
        return total;
    }
    [Benchmark]
    public object Part2()
    {
        int total = 0;
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            for (int y = 0; y < _grid.Length; y++)
            {
                for (int x = 0; x < _grid[y].Length; x++)
                {
                    if (_grid[y][x] == '@' && Solutions.Day4.CheckSurroundings(_grid, y, x) < 4)
                    {
                        _grid[y][x] = '.';
                        isDone = false;
                        total++;
                    }
                }
            }
        }
        return total;
    }
}
