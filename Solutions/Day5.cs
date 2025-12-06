using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

using Range = AdventOfCode2025.Solutions.Misc.Range;

namespace AdventOfCode2025.Solutions;

internal class Day5 : IDay
{
    public int Day { get; } = 5;
    public string Name { get; } = "Cafeteria";
    public string Part1Template { get; } = "[green]Answer:[/] The total number of fresh products in the database is {0}.";
    public string Part2Template { get; } = "[green]Answer:[/] The total number of fresh products in the database is {0}.";

    public object ParsePart1(string input)
    {
        string[] sections = input.Split(Environment.NewLine + Environment.NewLine);
        Range[] ranges = sections[0].Split(Environment.NewLine).Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(r =>
            {
                string[] split = r.Split('-');
                return new Range(long.Parse(split[0]), long.Parse(split[1]));
            }).ToArray();
        return (new RangeCollection(ranges), sections[1].Split('\n').Select(long.Parse).ToArray());
    }
    public object ParsePart2(string input) => ParsePart1(input);

    public object SolvePart1(object input)
    {
        (RangeCollection ranges, long[] products) = ((RangeCollection, long[]))input;
        long total = 0;
        foreach(var product in products)
        {
            if(ranges.IsInAnyInclusive(product))
            {
#if DEBUG
                AnsiConsole.MarkupLine($"[green]Product {product} is fresh[/]");
#endif
                total++;
            }
        }
        return total;
    }

    public object SolvePart2(object input)
    {
        (RangeCollection ranges, _) = ((RangeCollection, long[]))input;
        long total = 0;
        ranges.MergeOverlapping();
        total = ranges.Ranges
            .Select(r => r.Length + 1)
            .Sum();
        return total;
    }

    public class RangeCollection
    {
        private Range[] _ranges;
        public IReadOnlyCollection<Range> Ranges => _ranges;

        public RangeCollection(IEnumerable<Range> ranges, bool mergeOverlapping = false)
        {
            _ranges = ranges.ToArray();
            if (mergeOverlapping)
                MergeOverlapping();

        }
        public RangeCollection(Range[] ranges, bool mergeOverlapping = false)
        {
            _ranges = ranges;
            if (mergeOverlapping)
                MergeOverlapping();
        }

        internal void MergeOverlapping()
        {
            bool isDone = false;
            List<Range> ranges = _ranges.OrderBy(r => r.Start).ToList();
            List<HashSet<Range>> collections = [];
            while(!isDone)
            {
                isDone = true;
                foreach(var range in ranges)
                {

                    if (collections.Any(h => h.Contains(range)))
                        continue;
                    else
                    {
                        HashSet<Range> currentSet = [range];
                        collections.Add(currentSet);
                        foreach (var other in ranges.Where(r => r != range))
                            if (range.Overlaps(other))
                            {
                                currentSet.Add(other);
                                isDone = false;
                            }
                    }
                }
                if(!isDone)
                {
                    ranges = [];
                    foreach(var set in collections)
                    {
                        Range merged = default;
                        foreach(var r in set)
                        {
                            merged = merged == default ? r : merged.Merge(r);
                        }
                        ranges.Add(merged);
                    }
                    ranges = [.. ranges.OrderBy(r => r.Start)];
                    collections = [];
                }
            }
            _ranges = [.. ranges];
        }

        public bool IsInAnyInclusive(long number)
        {
            foreach (var range in _ranges)
                if (range.IsWithinInclusive(number))
                    return true;
            return false;
        }
        public bool IsInAnyExclusive(long number)
        {
            foreach (var range in _ranges)
                if (range.IsWithinExclusive(number))
                    return true;
            return false;
        }
    }
}
