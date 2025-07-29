namespace AoC2022;

public abstract class Day00
{
    // protected readonly string[] Input;
    //
    // protected Day00()
    // {
    //     var dayName = GetType().Name;
    //     var inputFilePath = Path.Combine(dayName, "input.txt");
    //
    //     if (!File.Exists(inputFilePath))
    //     {
    //         throw new FileNotFoundException("Input file not found", inputFilePath);
    //     }
    //     
    //     Input = File.ReadAllLines(inputFilePath);
    // }
    
    public abstract string Part1();
    public abstract string Part2();
}