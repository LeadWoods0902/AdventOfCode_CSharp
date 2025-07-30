namespace AoC2022.Day01;

// --- Advent of Code 2022 - Day 1: Calorie Counting ---
// 
// Input: List of calorie counts, grouped by Elf, separated by blank lines.

// Goal:
//   Part 1: Find the maximum calories carried by any single Elf.
//   Part 2: Sum the calories of the top 3 Elves carrying the most.
//
// My Answers:
//   Part 1: 71124
//   Part 2: 204639
public class Day01 : Day00
{
    private List<int>? _sortedList;
    
    // Part 1: Locate the sublist with the largest sum
    public override string Part1()
    {
        var input = File.ReadAllLines("input.txt");

        var sums = new List<int>();
        var currentSum = 0;
    
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (currentSum <= 0) continue;
                InsertDescending(sums, currentSum);
                currentSum = 0;
            }
            else
            {
                if (int.TryParse(line, out var value))
                {
                    currentSum += value;
                }
            }
        }

        _sortedList = sums;
        return $"{sums[0]}";
    }
    
    private static void InsertDescending(List<int>? sums, int currentSum)
    {
        var index = sums!.BinarySearch(currentSum, Comparer<int>.Create((a, b) => b.CompareTo(a)));
        if (index < 0) index = ~index;
        sums.Insert(index, currentSum);
    }
    
    // Part 2: Locate the three sublist with the largest sums
    public override string Part2()
    {
        var topThree = _sortedList!.Take(3).Sum();
        return $"{topThree}";
    }
}