namespace AoC2022.Day04;

public class Day04 : Day00
{
    private readonly (int minA, int maxA, int minB, int maxB)[] _ranges;

    public Day04()
    {
        _ranges = File.ReadAllLines("input.txt")
            .Select(line =>
            {
                var parts = line.Split(',', '-');
                return (
                    minA: int.Parse(parts[0]),
                    maxA: int.Parse(parts[1]),
                    minB: int.Parse(parts[2]),
                    maxB: int.Parse(parts[3])
                );
            })
            .ToArray();
    }
    public override string Part1()
    {
        var count = _ranges.Count(r =>
            (r.minA >= r.minB && r.maxA <= r.maxB) ||
            (r.minB >= r.minA && r.maxB <= r.maxA)
        );

        return count.ToString();
    }

    public override string Part2()
    {
        var count = _ranges.Count(r =>
            Math.Max(r.minA, r.minB) <= Math.Min(r.maxA, r.maxB)
        );

        return count.ToString();
    }
}