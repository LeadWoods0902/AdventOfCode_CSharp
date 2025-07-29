using System.Text;

namespace AoC2022.Day05;

public class Day05 : Day00
{
    private readonly List<Stack<char>> _buckets = Enumerable.Range(0, 9).Select(_ => new Stack<char>()).ToList();
    private readonly string[] _commands;
    
    public Day05()
    {
        var input = File.ReadAllLines("input.txt");
        var emptyLineIndex = Array.IndexOf(input, "");

        var startingPositions = input[..emptyLineIndex];
        _commands = input[(emptyLineIndex+1)..];

        for (var row = startingPositions.Length - 2; row >= 0; row--)
        {
            var line = startingPositions[row];
            for (var i = 0; i < 9; i++)
            {
                var col = 1 + i * 4;
                if (col < line.Length && char.IsLetter(line[col]))
                {
                    _buckets[i].Push(line[col]);
                }
            }
        }
    }
    public override string Part1()
    {
        var localBuckets = _buckets.Select(b => new Stack<char>(b.Reverse())).ToList();
        foreach (var command in _commands)
        {
            var parts = command.Split(" ");
            var count = int.Parse(parts[1]);
            var source = int.Parse(parts[3]) - 1;
            var destination = int.Parse(parts[5]) - 1;

            for (var i = 0; i < count; i++)
            {
                var crate = localBuckets[source].Pop();
                localBuckets[destination].Push(crate);
            }
        }

        var tops = new StringBuilder();
        foreach (var bucket in localBuckets)
        {
            if (bucket.Count > 0)
                tops.Append(bucket.Pop());
        }
        return tops.ToString();
    }

    public override string Part2()
    {
        var localBuckets = _buckets.Select(b => new Stack<char>(b.Reverse())).ToList();
        foreach (var command in _commands)
        {
            var parts = command.Split(" ");
            var count = int.Parse(parts[1]);
            var source = int.Parse(parts[3]) - 1;
            var destination = int.Parse(parts[5]) - 1;
            
            var stack = new Stack<char>();
            for (var i = 0; i < count; i++)
            {
                var crate = localBuckets[source].Pop();
                stack.Push(crate);
            }
            
            for (var i = 0; i < count; i++)
            {
                var crate = stack.Pop();
                localBuckets[destination].Push(crate);
            }
            
        }
        var tops = new StringBuilder();
        foreach (var bucket in localBuckets)
        {
            if (bucket.Count > 0)
                tops.Append(bucket.Pop());
        }
        return tops.ToString();
    }
}