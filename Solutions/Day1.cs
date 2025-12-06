using System;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal class Day1 : IDay
{
    public int Day { get; } = 1;
    public string Name { get; } = "Day 1: Secret Entrance";
    public string Part1Template { get; } = "[green]Part 1:[/] The number of times we land on 0 is {0}.";
    public string Part2Template { get; } = "[green]Part 2:[/] The number of clicks (land on 0 or wrap past 0) is {0}.";

    public object ParsePart1(string input)
    {
        return input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
    public object ParsePart2(string input)
    {
        return input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }


    public object SolvePart1(object input)
    {
        string[] lines = (string[])input;
        int currentNumber = 50;
        int zerosCount = 0;
        foreach (string line in lines)
        {
            int value = int.Parse(line[1..]);
            if (line[0] == 'L')
                value = -value;
#if DEBUG
            AnsiConsole.Markup($"Rotating {(value < 0 ? "Left" : "Right")} [cyan]{Math.Abs(value)}[/] units ");
#endif
            currentNumber = Wrap(currentNumber + value, 100);
#if DEBUG
            AnsiConsole.Markup($"(now [green]{currentNumber}[/])\n");
#endif
            if (currentNumber == 0)
                zerosCount++;
        }
        return zerosCount;
    }

    public object SolvePart2(object input)
    {
        string[] lines = (string[])input;
        int currentNumber = 50;
        int zerosCount = 0;
        foreach (string line in lines)
        {
            int value = int.Parse(line[1..]);
            if (line[0] == 'L')
                value = -value;
#if DEBUG
            AnsiConsole.Markup($"Rotating {(value < 0 ? "Left" : "Right")} [cyan]{Math.Abs(value)}[/] units ");
#endif
            currentNumber = BruteForceWrap(currentNumber, value, 0, 99, out int zeros);
            zerosCount += zeros;
#if DEBUG
            AnsiConsole.Markup($"(wrapped past 0 [yellow]{zeros}[/] time(s)) ");
            AnsiConsole.Markup($"(now [green]{currentNumber}[/])\n");
#endif
            if (currentNumber == 0)
                zerosCount++;
        }
        return zerosCount;
    }
    // Stolen from StackOverflow. I'll have to rewrite this later, all my own attempts wielded incorrect results and i couldn't figure out why,
    // despite the function itself being extremely simple in convept.
    public static int Wrap(int value, int modulo)
    {
        int remainder = (value % modulo);
        return (remainder < 0) ? (modulo + remainder) : remainder;
    }
    // This is so fucking disgusting. I really need to up my math game so i don't need to resort to this kind of stuff.
    public static int Wrap(int value, int rotation, int modulo, out int timesWrappedPast0)
    {
        int remainder = (value + rotation) % modulo;
        var wrapped = (remainder < 0) ? (modulo + remainder) : remainder;
        timesWrappedPast0 = Math.Abs(((value + rotation) - wrapped) / modulo);
        if(timesWrappedPast0 > 0 && (wrapped == 0 || value == 0))
            timesWrappedPast0--;
        return wrapped;
    }
    // For now i'll use this, but it'll help me understand why my math attempts failed.
    public static int BruteForceWrap(int value, int rotation, int min, int max, out int zeros)
    {
        int actions = Math.Abs(rotation);
        int zerosCount = 0;
        while (actions-- > 0)
        {
            if (rotation > 0)
            {
                value++;
                if (value > max)
                    value = min;
            }
            else
            {
                value--;
                if (value < min)
                    value = max;
            }
            if (value == 0)
                zerosCount++;

        }
        if(zerosCount > 0 && value == 0)
            zerosCount--;
        zeros = zerosCount;
        return value;
    }
}
