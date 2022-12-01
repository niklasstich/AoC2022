namespace AdventOfCode;

public class Day01 : BaseSolution
{
    public Day01() : base(01)
    {
    }

    public override async ValueTask<string> Solve_1() =>
        GetCalorieSums()
            .Max()
            .ToString();


    public override async ValueTask<string> Solve_2() => 
        GetCalorieSums()
            .OrderByDescending(i => i)
            .Take(3)
            .Sum()
            .ToString();

    private IEnumerable<int> GetCalorieSums()
    {
        return InputLines.Aggregate(new List<int>{0}, (ints, s) =>
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                ints.Add(0);
                return ints;
            }

            ints[^1] += int.Parse(s);
            return ints;
        });
    }

    public override string PartOneSolution => "69206";
    public override string PartTwoSolution => "197400";
}