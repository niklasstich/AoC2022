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
            .Select((s, i) => (s, i / 3))
            .GroupBy(tup => tup.Item2)
            .Select(UnpackStrings)
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

    private static IEnumerable<string> UnpackStrings(IGrouping<int, (string s, int)> arg) => arg.Select(tup => tup.s);

    private static char FindBadge(IEnumerable<string> arg) =>
        arg.Aggregate(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".AsEnumerable(),
                (chars, s) => chars.Intersect(s))
            .First();

    public override string PartOneSolution => "7826";
    public override string PartTwoSolution => "2577";
}