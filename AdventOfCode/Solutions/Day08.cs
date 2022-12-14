namespace AdventOfCode.Solutions;

public class Day08 : BaseSolution
{
    private int _rowLength;
    private int _colLength;

    public Day08() : base(08)
    {
    }

    public override async ValueTask<string> Solve_1()
    {
        _rowLength = InputLines.First().Length;
        _colLength = InputLines.Count();
        var others = InputLines
            .SelectMany(line => line.Select(c => c - '0'))
            .ToArray();
        var visibleInside = others
            .Where((height, i) => IsOnBorder(i) || IsVisible(height, i, others))
            .Count();
        return visibleInside.ToString();
    }

    private bool IsOnBorder(int index)
    {
        //border check
        var row = index / _rowLength;
        var col = index % _rowLength;
        return row == 0 || col == 0 || row == _colLength - 1 || col == _rowLength - 1;
    }

    private bool IsVisible(int height, int index, IReadOnlyList<int> others)
    {
        var row = index / _rowLength;
        var col = index % _rowLength;

        return VisibleFromAbove(height, others, row, col) ||
               VisibleFromBelow(height, others, row, col) ||
               VisibleFromLeft(height, others, col, row) ||
               VisibleFromRight(height, others, col, row);
    }

    private bool VisibleFromAbove(int height, IReadOnlyList<int> others, int row, int col)
    {
        for (var rowCheck = row + 1; rowCheck <= _colLength - 1; rowCheck++)
        {
            if (others[rowCheck * _rowLength + col] >= height) return false;
        }

        return true;
    }

    private bool VisibleFromBelow(int height, IReadOnlyList<int> others, int row, int col)
    {
        for (var rowCheck = row - 1; rowCheck >= 0; rowCheck--)
        {
            if (others[rowCheck * _rowLength + col] >= height) return false;
        }

        return true;
    }

    private bool VisibleFromLeft(int height, IReadOnlyList<int> others, int col, int row)
    {
        for (var colCheck = col - 1; colCheck >= 0; colCheck--)
        {
            if (others[row * _rowLength + colCheck] >= height) return false;
        }

        return true;
    }
    
    private bool VisibleFromRight(int height, IReadOnlyList<int> others, int col, int row)
    {
        for (var colCheck = col + 1; colCheck <= _rowLength - 1; colCheck++)
        {
            if (others[row * _rowLength + colCheck] >= height) return false;
        }

        return true;
    }

    public override async ValueTask<string> Solve_2()
    {
        //boring second part that prevents reuse, might do it in the future (probably not)
        return null;
    }

    public override string PartOneSolution => "1546";
    public override string PartTwoSolution { get; }
}