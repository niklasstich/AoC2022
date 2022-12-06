using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions;

public class Day05 : BaseSolution
{
    private static readonly Regex _moveRegex = new(@"move (\d*) from (\d*) to (\d*)", RegexOptions.Compiled);

    public Day05() : base(05)
    {
    }

    public override async ValueTask<string> Solve_1()
    {
        var stacks = ParseStackInput(InputLines.Take(8));
        var moves = ParseMoveInput(InputLines.Skip(10));
        ProcessMoves(stacks, moves);
        return string.Concat(stacks.Select(stack => stack.Peek()));
    }
    
    public override async ValueTask<string> Solve_2()
    {
        var stacks = ParseStackInput(InputLines.Take(8));
        var moves = ParseMoveInput(InputLines.Skip(10));
        ProcessMovesBatch(stacks, moves);
        return string.Concat(stacks.Select(stack => stack.Peek()));
    }

    private static Stack<char>[] ParseStackInput(IEnumerable<string> stackInput)
    {
        //create stacks
        var retval = Enumerable
            .Range(0, 9)
            .Select(_ => new Stack<char>())
            .ToArray();
        foreach (var line in stackInput)
        {
            for (var i = 0; i < 9; i++)
            {
                var c = line[1 + i * 4];
                if (c == ' ') continue;
                retval[i].Push(c);
            }
        }
        //reverse stacks
        return retval
            .Select(stack => new Stack<char>(stack))
            .ToArray();
    }

    private static IEnumerable<(int amount, int from, int to)> ParseMoveInput(IEnumerable<string> moveInput)
    {
        return moveInput
            .Select(line => _moveRegex.Match(line))
            .Select(match => match.Groups.Values.Skip(1).Select(cap => cap.Value))
            .Select(captures => captures.Select(int.Parse).ToArray())
            .Select(ints => (ints[0], ints[1] - 1, ints[2] - 1));
    }

    private static void ProcessMoves(IReadOnlyList<Stack<char>> stacks, IEnumerable<(int amount, int from, int to)> moves)
    {
        foreach (var (amount, from, to) in moves)
        {
            for (var i = 0; i < amount; i++)
            {
                stacks[to].Push(stacks[from].Pop());
            }
        }
    }

    private static void ProcessMovesBatch(IReadOnlyList<Stack<char>> stacks, IEnumerable<(int amount, int from, int to)> moves)
    {
        foreach (var (amount, from, to) in moves)
        {
            foreach (var c in stacks[from].Take(amount).Reverse())
            {
                var _ = stacks[from].Pop();
                stacks[to].Push(c);
            }
        }
    }

    public override string PartOneSolution => "RNZLFZSJH";
    public override string PartTwoSolution => "CNSFCGJSM";
}