using System.Reflection.Metadata.Ecma335;

namespace AoC2022.Day03;

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