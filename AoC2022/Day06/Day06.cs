namespace AoC2022.Day06;

public class Day06: Day00
{
    private readonly string _input = File.ReadAllLines("input.txt")[0];

    public override string Part1()
    {
        return FindMessage(4);
    }

    private string FindMessage(int windowSize)
    {
        var localInput = _input;
        var index = 0;
        for (var i = (windowSize-1); i < localInput.Length; i++)
        {
            var window = localInput.Substring(i-(windowSize-1), windowSize);
            if (window.Distinct().Count() != windowSize) continue;
            
            index = i+1;
            break;
        }
        return index.ToString();
    }

    public override string Part2()
    {
        return FindMessage(14);
    }
}