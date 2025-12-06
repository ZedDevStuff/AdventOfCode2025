using System.Collections.Generic;
using System.Text.RegularExpressions;

using Range = AdventOfCode2025.Solutions.Misc.Range;

namespace AdventOfCode2025.Solutions;

internal partial class Day2 : IDay
{
    public int Day { get; } = 2;
    public string Name { get; } = "Gift Shop";
    public string Part1Template { get; } = "[green]Answer:[/] The sum of all invalid IDs is: {0}";
    public string Part2Template { get; } = "[green]Answer:[/] The sum of all invalid IDs is: {0}";
    
    // I have been enlightened by the power of regex. My previous solution in *theory* did the same thing
    // but it was so overengineered that i didn't even know why it didn't work on my actual input.
    [GeneratedRegex(@"^(\d+)\1$", RegexOptions.Compiled)]
    public static partial Regex RepeatRegex();
    [GeneratedRegex(@"^(\d+)\1+$", RegexOptions.Compiled)]
    public static partial Regex RepeatMultipleRegex();
    public object ParsePart1(string input)
    {
        string[] ranges = input.Split(',');
        List<Range> tempRanges = [];
        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            tempRanges.Add(new(long.Parse(parts[0]), long.Parse(parts[1])));
        }
        return tempRanges.ToArray();
    }
    public object ParsePart2(string input) => ParsePart1(input);
    public object SolvePart1(object input)
    {
        Range[] ranges = (Range[])input;
        long total = 0;
        foreach (var range in ranges)
            for(long i = range.Start;i <= range.End;i++)
                if(RepeatRegex().IsMatch(i.ToString()))
                    total += i;
        return total;
    }

    public object SolvePart2(object input)
    {
        Range[] ranges = (Range[])input;
        long total = 0;
        foreach (var range in ranges)
            for (long i = range.Start; i <= range.End; i++)
                if (RepeatMultipleRegex().IsMatch(i.ToString()))
                    total += i;
        return total;
    }
}
