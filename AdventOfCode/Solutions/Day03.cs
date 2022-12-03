using System.Linq;

namespace AdventOfCode.Solutions;

public class Day03 : BaseSolution
{
    public Day03() : base(03)
    {
    }

    public override async ValueTask<string> Solve_1() =>
        InputLines
            .Select(SplitInHalf)
            .Select(Intersect)
            .Select(GetCharValue)
            .Sum()
            .ToString();

    public override async ValueTask<string> Solve_2() =>
        InputLines
            .Chunk(3)
            .Select(FindBadge)
            .Select(GetCharValue)
            .Sum()
            .ToString();

    private static (string, string) SplitInHalf(string str) => (str[.. (str.Length / 2)], str[(str.Length / 2) ..]);

    private static char Intersect((string, string) charcols) => charcols.Item1.Intersect(charcols.Item2).First();

    private static int GetCharValue(char c) =>
        c switch
        {
            >= 'a' and <= 'z' => c - 'a' + 1,
            >= 'A' and <= 'Z' => c - 'A' + 27,
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };

    private static char FindBadge(IEnumerable<string> arg) =>
        arg.Aggregate(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".AsEnumerable(),
                (chars, s) => chars.Intersect(s))
            .First();

    public override string PartOneSolution => "7826";
    public override string PartTwoSolution => "2577";
}