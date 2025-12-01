using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode2025;

internal class SolveCommand : AsyncCommand<SolveCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        if(Program.Days.TryGetValue(settings.Day, out var solution))
        {
            try
            {
                switch (settings.Part)
                {
                    case 1:
                        await solution.Setup(new ManifestEmbeddedFileProvider(typeof(Program).Assembly, $"Assets/Data/Day{settings.Day}"), settings.IsTesting);
                        await solution.SolvePart1();
                        break;
                    case 2:
                        await solution.Setup(new ManifestEmbeddedFileProvider(typeof(Program).Assembly, $"Assets/Data/Day{settings.Day}"), settings.IsTesting);
                        await solution.SolvePart2();
                        break;
                    default:
                        Console.WriteLine($"Part {settings.Part} is not valid. Choose 1 or 2.");
                        return 1;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occurred while solving the problem:[/] {ex.Message}");
                return 1;
            }
            
            return 0;
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Day {settings.Day} is not implemented yet.[/]");
            return 1;
        }
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<day>"), Description("The day of the Advent of Code challenge."), Range(1, 25)]
        public int Day { get; set; }
        [CommandArgument(1, "<part>"), Range(1, 2), Description("The part of the day's challenge to solve (1 or 2).")]
        public int Part { get; set; }
        [CommandOption("-t|--test"), Description("Loads test input data if available.")]
        public bool IsTesting { get; set; } = false;
    }
}
