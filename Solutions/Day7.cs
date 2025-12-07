using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AdventOfCode2025.Solutions.Misc;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

public class Day7 : IDay
{
    public int Day { get; } = 7;
    public string Name { get; } = "Laboratories";
    public string Part1Template { get; } = "[green]Answer:[/] The total amount of time the beam was split is {0}.";
    public string Part2Template { get; } = "[green]Answer:[/] The total amount of timelines is {0}.";
    
    public object ParsePart1(string input)
    {
        string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        Grid<char> grid = new Grid<char>(lines[0].Length, lines.Length);
        for (int y = 0; y < lines.Length; y++)
            grid.SetRow(y, lines[y].ToCharArray());
        return grid;
    }

    public object ParsePart2(string input) => ParsePart1(input);

    public object SolvePart1(object input)
    {
        Grid<char> grid = (Grid<char>)input;
        long total = 0;
        bool done = false;
        int startX = grid.IndexOfInRow(0, 'S');
        List<int> currentPos = [startX];
        List<int> splits = [];
        for (int y = 1; y < grid.Height && !done; y++)
        {
            List<int> nextPos = [];
            foreach (var x in currentPos)
            {
                if (grid[x, y] == '.')
                {
#if DEBUG
                    grid[x, y] = '|';
#endif
                    nextPos.Add(x);
                }
                else if (grid[x, y] == '^')
                {
                    nextPos.Add(x - 1);
                    nextPos.Add(x + 1);
                    total++;
                }
            }
            currentPos = nextPos.Distinct().ToList();
        }
#if DEBUG
        grid.PrintGridCompact(c => c);
#endif
        splits = splits.Distinct().ToList();
        return total;
    }
    // Not working yet.
    public object SolvePart2(object input)
    {
        Grid<char> grid = (Grid<char>)input;
        bool done = false;
        int startX = grid.IndexOfInRow(0, 'S');
        int rowsOfSplitters = grid.AsRows().Count(r => r.Contains('^'));
        char[] directions = new char[rowsOfSplitters];
        Array.Fill(directions, 'L');
        List<int> currentPos = [startX];
        long timelines = 0;
        while (!done)
        {
            timelines++;
            int splitterIndex = 0;
            for (int y = 1; y < grid.Height && !done; y++)
            {
                List<int> nextPos = [];
                foreach (var x in currentPos)
                {
                    if (grid[x, y] == '.')
                    {
#if DEBUG
                        grid[x, y] = '|';
#endif
                        nextPos.Add(x);
                    }
                    else if (grid[x, y] == '^')
                    {
                        if (directions[splitterIndex] == 'L')
                            nextPos.Add(x - 1);
                        else
                            nextPos.Add(x + 1);
                        splitterIndex++;
                    }
                }
                currentPos = nextPos.Distinct().ToList();
            }
#if DEBUG
            grid.PrintGridCompact(c => c);
#endif
            if (directions.All(d => d == 'R'))
            {
                done = true;
            }
            else
            {
                for (int i = directions.Length - 1; i >= 0; i--)
                {
                    if (directions[i] == 'L')
                    {
                        directions[i] = 'R';
                        break;
                    }
                    else
                    {
                        directions[i] = 'L';
                    }
                }
            }
        }
        return timelines;
    }
}
