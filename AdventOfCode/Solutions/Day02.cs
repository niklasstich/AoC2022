namespace AdventOfCode.Solutions;

public class Day02 : BaseSolution
{
    private readonly int[] _lookup;

    public Day02() : base(02)
    {
        //self\enemy rock  paper  scissors
        //rock -     draw, lose,  win
        //paper -    win,  draw,  lose
        //scissors - lose, win,   draw
        _lookup = new[] { 4, 1, 7, 8, 5, 2, 3, 9, 6 };
    }

    public override async ValueTask<string> Solve_1()
    {
        return InputLines
            .Select(Split)
            .Select(ConvertToCharTuple)
            .Select(GetPartOneResult)
            .Sum()
            .ToString();
    }


    private int GetPartOneResult((char, char) arg)
    {
        //enemy - 'A' = column 
        //(self - 'X') * 3 = row
        return _lookup[arg.Item1 - 'A' + 3 * (arg.Item2 - 'X')];
    }
    

    public override async ValueTask<string> Solve_2()
    {
        return InputLines
            .Select(Split)
            .Select(ConvertToCharTuple)
            .Select(GetPartTwoResult)
            .Sum()
            .ToString();
    }

    private int GetPartTwoResult((char, char) arg)
    {
        //self\enemy   rock      paper     scissors
        //lose         scissors  rock      paper
        //draw         rock      paper     scissors
        //win          paper     scissors  rock
        //enemy - same as part 1
        //self - shifted right by 2, hence add two to self and add enemy pick
        return _lookup[arg.Item1 - 'A' + 3 * ((arg.Item1 - 'A' + (arg.Item2 - 'X' + 2) % 3) % 3)];
    }

    private static string[] Split(string str)
    {
        return str.Split(' ');
    }

    private static (char, char) ConvertToCharTuple(string[] arr)
    {
        return (arr[0][0], arr[1][0]);
    }
    public override string PartOneSolution => "8890";
    public override string PartTwoSolution => "10238";
}