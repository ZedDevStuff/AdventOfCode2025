using System.Threading.Tasks;

using Microsoft.Extensions.FileProviders;

namespace AdventOfCode2025.Solutions;

public interface ISolution
{
    public int Day { get; }
    public string Name { get; }

    public string FullName => $"Day {Day}: {Name}";

    public Task Setup(ManifestEmbeddedFileProvider files, bool isTesting);
    public Task SolvePart1();
    public Task SolvePart2();

}
