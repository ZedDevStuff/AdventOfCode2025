using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

using Range = AdventOfCode2025.Solutions.Misc.Range;

namespace AdventOfCode2025.Solutions;

internal class Day5 : ISolution
{
    public int Day { get; } = 5;
    public string Name { get; } = "Cafeteria";
    private bool _isTesting;
    private RangeCollection _ranges = null!;
    private long[] _products = [];
    public async Task Setup(ManifestEmbeddedFileProvider files, bool isTesting)
    {
        _isTesting = isTesting;
        string[] sections = files.GetFileInfo(isTesting ? "test.txt" : "input.txt").CreateReadStream().ReadToEnd().Split("\r\n\r\n");
        Range[] ranges = sections[0].Split("\r\n").Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(r =>
            {
                string[] split = r.Split('-');
                return new Range(long.Parse(split[0]), long.Parse(split[1]));
            }).ToArray();
        _ranges = new(ranges);
        _products = [.. sections[1].Split('\n').Select(long.Parse)];
    }

    public async Task SolvePart1()
    {
        long total = 0;
        foreach(var product in _products)
        {
            if(_ranges.IsInAnyInclusive(product))
            {
                if (_isTesting)
                    AnsiConsole.MarkupLine($"[green]Product {product} is fresh[/]");
                total++;
            }
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total number of fresh products in the database is {total}.");
    }

    public async Task SolvePart2()
    {
        long total = 0;
        _ranges.MergeOverlapping();
        total = _ranges.Ranges
            .Select(r => r.Length + 1)
            .Sum();
        AnsiConsole.MarkupLine($"[green]Answer:[/] The total number of fresh products in the database is {total}.");
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
