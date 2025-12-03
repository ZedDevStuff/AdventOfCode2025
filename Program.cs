using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AdventOfCode2025.Solutions;

using Microsoft.Extensions.FileProviders;

using Spectre.Console.Cli;

namespace AdventOfCode2025;

internal class Program
{
    internal static ManifestEmbeddedFileProvider Files = new(typeof(Program).Assembly, "Assets/Data");
    internal static Dictionary<int, ISolution> Days = [];
    static async Task Main(string[] args)
    {
        ISolution[] solutions = typeof(Program).Assembly.GetTypes()
            .Where(t => !t.IsInterface && t.IsAssignableTo(typeof(ISolution)))
            .OrderBy(t => t.Name)
            .Select(t => (ISolution)Activator.CreateInstance(t)!)
            .ToArray()!;
        foreach (ISolution solution in solutions)
            Days[solution.Day] = solution;
        CommandApp app = new();
        app.Configure(config =>
        {
            config.AddCommand<SolveCommand>("solve")
                .WithExample(["solve", "1"])
                .WithExample(["solve", "15", "2"]);
            config.AddCommand<ListCommand>("list");
        });
        await app.RunAsync(args);
    }

}
