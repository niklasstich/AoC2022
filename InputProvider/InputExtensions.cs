namespace InputProvider;

public static class InputExtensions
{
    public static string GetInput(int day) => File.ReadAllText(GetPath(day));
    public static IEnumerable<string> GetInputLines(int day) => File.ReadAllLines(GetPath(day));
    private static string GetPath(int day) => Path.Combine("Inputs", $"{day:00}.txt");
}