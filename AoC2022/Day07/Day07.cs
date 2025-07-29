using System.Text.RegularExpressions;

namespace AoC2022.Day07;

public class DirectoryNode(string name, DirectoryNode? parent)
{
    public string Name { get; } = name;
    public DirectoryNode? Parent { get; } = parent;
    public List<DirectoryNode> Children { get; } = [];
    public List<FileEntry> Files { get; } = [];
    public int GetTotalSize()
    {
        var fileSize = Files.Sum(f => f.Size);
        var childSize = Children.Sum(c => c.GetTotalSize());
        return fileSize + childSize;
    }
}

public class FileEntry(string name, int size)
{
    public string Name { get; } = name;
    public int Size { get; } = size;
}

public class Day07 : Day00
{
    private readonly DirectoryNode _root = new("/", null);
    private List<KeyValuePair<string, int>> _allDirectories = [];
    public override string Part1()
    {
        var input = File.ReadAllLines("input.txt");
        
        var current = _root;

        foreach (var line in input)
        {
            if (line.StartsWith("$ cd"))
            {
                var target = line.Substring("$ cd ".Length);
                current = target switch
                {
                    "/" => _root,
                    ".." => current!.Parent ?? current,
                    _ => current!.Children.FirstOrDefault(c => c.Name == target)
                };
            }
            else if (line.StartsWith("dir "))
            {
                var dirName = line.Substring("dir ".Length);
                if (current!.Children.All(c => c.Name != dirName))
                {
                    current.Children.Add(new DirectoryNode(dirName, current));
                }
            }
            else if (Regex.IsMatch(line, @"^\d+ "))
            {
                var parts = line.Split(' ');
                var size = int.Parse(parts[0]);
                var fileName = parts[1];
                current!.Files.Add(new FileEntry(fileName, size));
            }
        }
        
        // ShowDirectoryStructure(root);
        _allDirectories = CalculateDirectorySizes(_root);
        var sum = _allDirectories.Where(kvp => kvp.Value < 100000).Sum(kvp => kvp.Value);
        return sum.ToString();
    }

    private static void ShowDirectoryStructure(DirectoryNode node, string indent = "")
    {
        Console.WriteLine($"{indent}- {node.Name} (dir, total size={node.GetTotalSize()})");

        foreach (var file in node.Files)
        {
            Console.WriteLine($"{indent}  - {file.Name} (file, size={file.Size})");
        }

        foreach (var child in node.Children)
        {
            ShowDirectoryStructure(child, indent + "  ");
        }
    }

    private static List<KeyValuePair<string, int>> CalculateDirectorySizes(DirectoryNode node)
    {
        var directorySizes = new List<KeyValuePair<string, int>>();
        int totalSize = 0;
        
        foreach (var file in node.Files)
        {
            totalSize += file.Size;
        }
        
        foreach (var child in node.Children)
        {
            var childResults = CalculateDirectorySizes(child);
            directorySizes.AddRange(childResults);
            
            var childTotal = childResults.FirstOrDefault(kvp => kvp.Key == child.Name).Value;
            totalSize += childTotal;
        }
        
        directorySizes.Add(new KeyValuePair<string, int>(node.Name, totalSize));
        
        return directorySizes;
    }

    public override string Part2()
    {
        List<int> validDirs = [];
        var root = _allDirectories.FirstOrDefault(kvp => kvp.Key == "/");
        var rootSize = root.Value;
        foreach (var dir in _allDirectories)
        {
            if (rootSize - dir.Value >= 40000000) continue;
            
            validDirs.Add(dir.Value);
        }
        validDirs.Sort();
        return validDirs[0].ToString();
    }
}