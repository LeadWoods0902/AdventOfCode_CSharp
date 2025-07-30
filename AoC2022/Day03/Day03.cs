namespace AoC2022.Day03;

// --- Advent of Code 2022 - Day 3: Rucksack Reorganization ---
//
// Input: Each line represents the contents of an elf's backpack.
// Each backpack has two compartments, the contents are split equally
// between the two compartments.
//
// Goal:
//  Part 1:
//   - Find the one item type that appears in both compartments of the rucksack.
//   - Sum the priorities of all such items. Where item priorities are:
//     - a-z = 1–26,
//     - A-Z = 27–52.
//
// Part 2:
//   - Elves are now grouped in sets of three.
//   - Find the one item type that appears in all three rucksacks.
//   - Sum the priorities of all found items.
//
// My Answers:
//   Part 1: 7848
//   Part 2: 2616

public class Day03 : Day00
{
    public override string Part1()
    {
        var input = File.ReadAllLines("input.txt");

        var backpacksScore = 0;

        foreach (var line in input)
        {
            var firstComp = new HashSet<char>(line[..(line.Length/2)]);
            var secondComp = new HashSet<char>(line[(line.Length/2)..]);
            
            firstComp.IntersectWith(secondComp);
            
            backpacksScore += MapCharToRange(firstComp.First());
        }

        return backpacksScore.ToString();
    }

    private static int MapCharToRange(char c)
    {
        if (char.IsLower(c))
            return c - 'a' + 1;
        if (char.IsUpper(c))
            return c - 'A' + 27;
        throw new ArgumentException($"Invalid Input: {c}");
    }

    public override string Part2()
    {
        var input = File.ReadAllLines("input.txt");

        var backpacksScore = 0;
        
        for (var line = 0; line < input.Length; line += 3)
        {
            var firstBag = new HashSet<char>(input[line]);
            var secondBag = new HashSet<char>(input[line+1]);
            var thirdBag = new HashSet<char>(input[line+2]);
            
            firstBag.IntersectWith(secondBag);
            firstBag.IntersectWith(thirdBag);
            
            backpacksScore += MapCharToRange(firstBag.First());
        }

        return backpacksScore.ToString();
    }
}