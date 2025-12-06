using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AdventOfCode2025.Benchmarks;
using AdventOfCode2025.Solutions;

using Microsoft.Extensions.FileProviders;

using Spectre.Console.Cli;

namespace AdventOfCode2025;

internal class Program
{
    internal static ManifestEmbeddedFileProvider Files = new(typeof(Program).Assembly, "Assets/Data");
    internal static Dictionary<int, IDay> Days = [];
    static async Task Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--benchmark")
            BenchmarkProgram.Run(args.Skip(1).ToArray());
        else
        {
            IDay[] solutions = typeof(Program).Assembly.GetTypes()
                .Where(t => !t.IsInterface && t.IsAssignableTo(typeof(IDay)) && !t.IsAbstract)
                .OrderBy(t => t.Name)
                .Select(t => (IDay)Activator.CreateInstance(t)!)
                .ToArray()!;
            foreach (IDay solution in solutions)
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

}
