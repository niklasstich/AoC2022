using InputProvider;

namespace AdventOfCode;

public abstract class BaseSolution : BaseDay, ISolveable
{
    public BaseSolution(int day)
    {
        Input = InputExtensions.GetInput(day);
        InputLines = InputExtensions.GetInputLines(day);
    }
    public string Input { get; }
    public IEnumerable<string> InputLines { get; }
    public abstract string PartOneSolution { get; }
    public abstract string PartTwoSolution { get; }
}