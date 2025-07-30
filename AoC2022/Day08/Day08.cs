namespace AoC2022.Day08;

public class Day08 : Day00
{
    public override string Part1()
    {
        var input = File.ReadAllLines("input.txt");

        var forestHeight = input.Length;
        var forestWidth = input[0].Length;

        var visibleTrees = 2*(forestWidth + forestHeight) - 4;
        
        var directions = new (int dx, int dy)[]
        {
            (-1,  0),
            ( 1,  0),
            ( 0, -1),
            ( 0,  1) 
        };

        for (var x = 1; x < forestWidth-1; x++)
        {
            for (var y = 1; y < forestHeight-1; y++)
            {
                var currentTree = input[y][x];
                
                // Console.WriteLine($"Checking Tree @ ({x},{y}):{currentTree}");
                
                foreach (var (dx, dy) in directions)
                {
                    if (!IsVisibleInDirection(input, x, y, dx, dy, currentTree))
                    {
                        // Console.WriteLine("---- Hidden ----");
                        continue;
                    }
                    
                    visibleTrees++;
                    break;
                }
            }
        }
        
        return visibleTrees.ToString();
    }
    
    private bool IsVisibleInDirection(string[] grid, int x, int y, int dx, int dy, char current)
    {
        var width = grid[0].Length;
        var height = grid.Length;

        x += dx;
        y += dy;

        while (x >= 0 && x < width && y >= 0 && y < height)
        {
            // Console.WriteLine($"Comparing w/ ({x},{y}):{grid[y][x]}");
            if (grid[y][x] >= current)
                return false;

            x += dx;
            y += dy;
        }

        // Console.WriteLine("---- Visible ----");
        return true;
    }

    public override string Part2()
    {
        throw new NotImplementedException();
    }
}