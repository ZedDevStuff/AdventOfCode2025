using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode2025;

[Description("Lists all implemented Advent of Code 2025 solutions")]
public class ListCommand : Command
{
    public override int Execute(CommandContext context, CancellationToken cancellationToken)
    {
        foreach(var solution in Program.Days.Values)
        {
            AnsiConsole.MarkupLine($"[green]Day {solution.Day}[/]: {solution.Name}");
        }
        return 0;
    }
}
