using System.Reflection;

namespace AoC2022;

internal static class Program
{
    private static void Main(string[] args)
    {
        var day = args[0];
        var className = $"AoC2022.Day{day}.Day{day}";

        try
        {
            var asm = Assembly.GetExecutingAssembly();
            var dayType = asm.GetType(className);

            if (dayType == null)
            {
                Console.WriteLine($"Could not find class {className}");
                return;
            }
            
            var dayInstance = Activator.CreateInstance(dayType) as Day00;

            if (dayInstance == null)
            {
                Console.WriteLine($"Class {className} does not inherit from Day00 or could not be instantiated.");
                return;
            }

            // Run the solution
            Console.WriteLine($"Running Day{day}");
            Console.WriteLine($"Part 1: {dayInstance.Part1()}");
            Console.WriteLine($"Part 2: {dayInstance.Part2()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running {className}: {ex.Message}");
        }
    }
}