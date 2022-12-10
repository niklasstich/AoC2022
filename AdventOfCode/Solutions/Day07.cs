namespace AdventOfCode.Solutions;

/// <summary>
/// Completely overengineered solution parsing the input as a tree structure, when all we really needed in the end
/// was a list. Disappointed with this day.
/// </summary>
public class Day07 : BaseSolution
{
    private interface IFileSystemNode
    {
        string Name { get; }
        int Size { get; }
    }

    private class Directory : IFileSystemNode
    {
        public Directory(string name, Directory? parent)
        {
            Name = name;
            Parent = parent;
            Contents = new List<IFileSystemNode>();
        }

        public string Name { get; }
        public Directory? Parent { get; }
        public int Size => Contents.Sum(node => node.Size);
        public ICollection<IFileSystemNode> Contents { get; }
    }
    
    private class File : IFileSystemNode
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }
        public string Name { get; }
        public int Size { get; }
    }

    public Day07() : base(07)
    {
    }
    
    public override async ValueTask<string> Solve_1()
    {
        return FlattenFolderStructure(ParseInput(InputLines))
            .Select(dir => dir.Size)
            .Where(size => size <= 100_000)
            .Sum()
            .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        const int MAX_SIZE = 70_000_000;
        const int FREE_SPACE_REQUIRED = 30_000_000;
        var root = ParseInput(InputLines);
        return FlattenFolderStructure(root)
            .Select(dir => dir.Size)
            .Where(size => size >= FREE_SPACE_REQUIRED - (MAX_SIZE - root.Size))
            .Min()
            .ToString();
    }

    private Directory ParseInput(IEnumerable<string> inputLines)
    {
        var rootDirectory = new Directory("/", null);
        return inputLines.Aggregate((rootDirectory, rootDirectory), ParseInputInner).Item1;
    }

    private (Directory root, Directory current) ParseInputInner((Directory root, Directory current) tuple, string s)
    {
        var split = s.Split();
        switch (split[0])
        {
            case "$":
                return ParseCommand(tuple, split[1..]);
            case "dir":
                return ParseDirectoryEntry(tuple, split[1]);
        }

        if (int.TryParse(split[0], out var baz)) return ParseFileEntry(tuple, split[1], baz);
        throw new NotSupportedException();
    }

    private (Directory root, Directory current) ParseDirectoryEntry((Directory root, Directory current) tuple, string s)
    {
        tuple.current.Contents.Add(new Directory(s, tuple.current));
        return tuple;
    }

    private (Directory root, Directory current) ParseFileEntry((Directory root, Directory current) tuple, string s, int baz)
    {
        tuple.current.Contents.Add(new File(s, baz));
        return tuple;
    }

    private (Directory root, Directory current) ParseCommand((Directory root, Directory current) tuple, IReadOnlyList<string> strings)
    {
        if (strings[0] == "cd") return SetDirectory(tuple, strings[1]);
        if (strings[0] != "ls") throw new Exception("brother hurb");
        return tuple;
    }

    private (Directory root, Directory current) SetDirectory((Directory root, Directory current) tuple, string directoryStr)
    {
        switch (directoryStr)
        {
            case "..":
                tuple.current = tuple.current.Parent!;
                return tuple;
            case "/":
                tuple.current = tuple.root;
                break;
            default:
                tuple.current = tuple.current.Contents.OfType<Directory>().First(directory => directory.Name == directoryStr);
                break;
        }
        return tuple;
    }

    private IEnumerable<Directory> FlattenFolderStructure(Directory root)
    {
        var retval = new List<Directory>();
        var stack = new Stack<Directory>();
        stack.Push(root);
        retval.Add(root);
        while (stack.Any())
        {
            var current = stack.Pop();
            retval.AddRange(current.Contents.OfType<Directory>());
            foreach (var dirWithChildDirs in current.Contents.OfType<Directory>().Where(dir => dir.Contents.OfType<Directory>().Any()))
            {
                stack.Push(dirWithChildDirs);
            }
        }

        return retval;
    }


    public override string PartOneSolution => "1141028";
    public override string PartTwoSolution => "8278005";
}