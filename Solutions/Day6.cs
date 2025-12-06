using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using static AdventOfCode2025.Solutions.Day6;

namespace AdventOfCode2025.Solutions;

public partial class Day6 : IDay
{
    public int Day { get; } = 6;
    public string Name { get; } = "Trash Compactor";
    public string Part1Template { get; } = "[green]Answer:[/] The grand total found by adding all answers is {0}.";
    public string Part2Template { get; } = "[green]Answer:[/] The grand total found by adding all answers is {0}.";

    [GeneratedRegex(@"[\d]+\s+", RegexOptions.Compiled)]
    public static partial Regex CellSelector();
    public object ParsePart1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var elements = lines.SelectMany(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        int height = lines.Length;
        int width = elements.Count() / height;
        Cell[][] table = new Cell[width][];
        for (int x = 0; x < width; x++)
        {
            var column = new Cell[height];
            for (int y = 0; y < height; y++)
            {
                string element = elements.ElementAt(y * width + x);
                Cell cell = new();
                if (long.TryParse(element, out long number))
                {
                    cell.Value = number;
                }
                else if (element == "+")
                {
                    cell.Content = Cell.ContentType.Add;
                }
                else if (element == "*")
                {
                    cell.Content = Cell.ContentType.Multiply;
                }
                else
                {
                    throw new InvalidOperationException();
                }
                column[y] = cell;
            }
            table[x] = column;
        }
        return table;
    }
    public object ParsePart2(string input)
    {
        string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        int height = lines.Length;
        int width = lines[0].Length;
        List<List<Cell>> table = [];
        int columnCount = 0;
        for (int x = width - 1; x >= 0; x--)
        {
            string[] column = new string[height];
            for(int i = 0;i < height;i++)
            {
                column[i] = lines[i][x].ToString();
            }
            if(column.All(c => c == " "))
            {
                continue;
            }
            int number = int.Parse(column.SkipLast(1).Aggregate((a, b) => a + b));
            if(table.Count <= columnCount)
                table.Add([]);
            table[columnCount].Add(new Cell(number));
            if (column[^1] == "+")
            {
                table[columnCount].Add(new Cell(Cell.ContentType.Add));
                columnCount++;
            }
            else if (column[^1] == "*")
            {
                table[columnCount].Add(new Cell(Cell.ContentType.Multiply));
                columnCount++;
            }
        }
        return table;
    }
    public object SolvePart1(object input)
    {
        Cell[][] table = (Cell[][])input;
        long total = 0;
        for(int x = 0;x < table.Length; x++)
        {
            total += SolveCollumn(table[x]);
        }
        return total;
    }
    public object SolvePart2(object input)
    {
        List<List<Cell>> table = (List<List<Cell>>)input;
        long total = 0;
        for (int x = 0; x < table.Count; x++)
        {
            total += SolveCollumn(table[x]);
        }
        return total;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>This assumes that the last cell is a symbol</remarks>
    /// <param name="cells"></param>
    /// <returns></returns>
    public static long SolveCollumn(Cell[] cells)
    {
        Cell.ContentType symbol = cells[^1].Content;
        return symbol switch
        {
            Cell.ContentType.Add => cells[..^1].Sum(c => c.Value),
            Cell.ContentType.Multiply => Mul(cells[..^1])

        };
    }
    public static long SolveCollumn(List<Cell> cells)
    {
        Cell.ContentType symbol = cells[^1].Content;
        return symbol switch
        {
            Cell.ContentType.Add => cells.SkipLast(1).Sum(c => c.Value),
            Cell.ContentType.Multiply => Mul(cells.SkipLast(1))
        };
    }

    public static long Mul(Cell[] input)
    {
        long total = 1;
        foreach(var cell in input)
        {
            total *= cell.Value;
        }
        return total;
    }
    public static long Mul(IEnumerable<Cell> input)
    {
        long total = 1;
        foreach (var cell in input)
        {
            total *= cell.Value;
        }
        return total;
    }

    public struct Cell
    {
        public long Value;
        public ContentType Content;

        public Cell(long value)
        {
            Value = value;
            Content = ContentType.Number;
        }
        public Cell(ContentType content)
        {
            Value = 0;
            Content = content;
        }
        public enum ContentType
        {
            Number,
            Add,
            Multiply
        }
    }

}
