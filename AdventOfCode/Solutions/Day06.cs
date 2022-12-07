namespace AdventOfCode.Solutions;

public class Day06 : BaseSolution
{
    public Day06() : base(06)
    {
    }

    public override async ValueTask<string> Solve_1() => FirstMarker(Input, 4);

    public override async ValueTask<string> Solve_2() => FirstMarker(Input, 14);
    
    private string FirstMarker(string input, int distinctCount)
    {
        for (var i = 0; i < input.Length-distinctCount; i++)
        {
            if (input.Skip(i).Take(distinctCount).Distinct().Count() == distinctCount)
            {
                return (i+distinctCount).ToString();
            }
        }

        throw new Exception("No marker in string");
    }

    public override string PartOneSolution => "1920";
    public override string PartTwoSolution => "2334";
}