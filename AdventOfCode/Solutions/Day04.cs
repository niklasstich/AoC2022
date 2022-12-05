namespace AdventOfCode.Solutions;

public class Day04 : BaseSolution
{
    public Day04() : base(04)
    {
    }

    public override async ValueTask<string> Solve_1()
    {
        return InputLines
            .Select(ParseToTuples)
            .Count(arr => LeftInRight(arr) || RightInLeft(arr))
            .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        return InputLines
            .Select(ParseToTuples)
            .Count(Intersect)
            .ToString();
    }

    private bool Intersect((int lower, int upper)[] arr)
    {
        
        return Math.Max(arr[0].lower, arr[1].lower) <= Math.Min(arr[0].upper, arr[1].upper);
    }

    private bool LeftInRight(IList<(int lower, int upper)> arr)
    {
        return arr[0].lower >= arr[1].lower && arr[0].upper <= arr[1].upper;
    }
    private bool RightInLeft(IList<(int lower, int upper)> arr)
    {
        return arr[1].lower >= arr[0].lower && arr[1].upper <= arr[0].upper;
    }

    private (int lower, int upper)[] ParseToTuples(string arg)
    {
        return arg
            .Split(",")
            .Select(substr => substr.Split("-").Select(int.Parse).ToArray())
            .Select(arr => (arr.ElementAt(0), arr.ElementAt(1)))
            .ToArray();
    }

    public override string PartOneSolution => "573";
    public override string PartTwoSolution => "867";
}