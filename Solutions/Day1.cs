using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;

namespace AdventOfCode2025.Solutions;

internal class Day1 : ISolution
{
    public int Day { get; } = 1;
    public string Name { get; } = "Day 1: Secret Entrance";
    private bool _isTesting = false;
    private string[] _lines = [];

    public async Task Setup(ManifestEmbeddedFileProvider files, bool isTesting)
    {
        _isTesting = isTesting;
        _lines = files.GetFileInfo(isTesting ? "test.txt" : "input.txt").CreateReadStream().ReadLines();
    }

    public async Task SolvePart1()
    {
        int currentNumber = 50;
        int zerosCount = 0;
        foreach (string line in _lines)
        {
            int value = int.Parse(line[1..]);
            if (line[0] == 'L')
                value = -value;
            if (_isTesting)
                AnsiConsole.Markup($"Rotating {(value < 0 ? "Left" : "Right")} [cyan]{Math.Abs(value)}[/] units ");
            currentNumber = Wrap(currentNumber + value, 100);
            if (_isTesting)
                AnsiConsole.Markup($"(now [green]{currentNumber}[/])\n");
            if (currentNumber == 0)
                zerosCount++;
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The number of times we land on 0 is {zerosCount}.");
        
    }

    public async Task SolvePart2()
    {
        int currentNumber = 50;
        int zerosCount = 0;
        foreach (string line in _lines)
        {
            int value = int.Parse(line[1..]);
            if (line[0] == 'L')
                value = -value;
            if (_isTesting)
                AnsiConsole.Markup($"Rotating {(value < 0 ? "Left" : "Right")} [cyan]{Math.Abs(value)}[/] units ");
            //currentNumber = Wrap(currentNumber, value, 100, out int zeros);
            currentNumber = BruteForceWrap(currentNumber, value, 0, 99, out int zeros);
            zerosCount += zeros;
            if (_isTesting)
                AnsiConsole.Markup($"(wrapped past 0 [yellow]{zeros}[/] time(s)) ");
            if (_isTesting)
                AnsiConsole.Markup($"(now [green]{currentNumber}[/])\n");
            if (currentNumber == 0)
                zerosCount++;
        }
        AnsiConsole.MarkupLine($"[green]Answer:[/] The number of clicks (land on 0 or wrap past 0) is {zerosCount}.");
    }
    // Stolen from StackOverflow. I'll have to rewrite this later, all my own attempts wielded incorrect results and i couldn't figure out why,
    // despite the function itself being extremely simple in convept.
    public int Wrap(int value, int modulo)
    {
        int remainder = (value % modulo);
        return (remainder < 0) ? (modulo + remainder) : remainder;
    }
    // This is so fucking disgusting. I really need to up my math game so i don't need to resort to this kind of stuff.
    public int Wrap(int value, int rotation, int modulo, out int timesWrappedPast0)
    {
        int remainder = ((value + rotation) % modulo);
        var wrapped = (remainder < 0) ? (modulo + remainder) : remainder;
        timesWrappedPast0 = Math.Abs(((value + rotation) - wrapped) / modulo);
        if(timesWrappedPast0 > 0 && (wrapped == 0 || value == 0))
            timesWrappedPast0--;
        return wrapped;
    }
    // For now i'll use this, but it'll help me understand why my math attempts failed.
    public int BruteForceWrap(int value, int rotation, int min, int max, out int zeros)
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
