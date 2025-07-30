namespace AoC2022.Day02;

// --- Advent of Code 2022 - Day 2: Rock Paper Scissors ---
//
// Input: Each line represents a round of Rock-Paper-Scissors.
//   - Opponent's move: A = Rock, B = Paper, C = Scissors
//   - Your move (Part 1): X = Rock, Y = Paper, Z = Scissors
//   - Desired outcome (Part 2): X = Lose, Y = Draw, Z = Win
//
// Scoring:
//   - Shape: Rock = 1, Paper = 2, Scissors = 3
//   - Outcome: Loss = 0, Draw = 3, Win = 6
//   - Round Score = Shape + Outcome (i.e. plays paper and wins => 2 + 6 = 8)
//
// Goal:
//   Part 1: Calculate total score assuming the second column is your move.
//   Part 2: Calculate total score assuming the second column is the desired outcome.
//
// My Answers:
//   Part 1: 13446
//   Part 2: 13509

public class Day02: Day00
{
    /* A, X = Rock
     * B, Y = Paper
     * C, Z = Scissors
    */
    private static readonly Dictionary<(string Opp, string Play), int> ScoreTable = new()
    {
        { ("A", "X"), 4 }, //Draw, Rock
        { ("A", "Y"), 8 }, //Win, Paper
        { ("A", "Z"), 3 }, //Loss, Scissors
        { ("B", "X"), 1 }, //Loss, Rock
        { ("B", "Y"), 5 }, //Draw, Paper
        { ("B", "Z"), 9 }, //Win, Scissors
        { ("C", "X"), 7 }, //Win, Rock
        { ("C", "Y"), 2 }, //Loss, Paper
        { ("C", "Z"), 6 }, //Draw, Scissors
    };
    
    /* X = lose
     * Y = draw
     * Z = win
     */
    private static readonly Dictionary<(string Opp, string Play), int> StrategyTable = new()
    {
        { ("A", "X"), 3 }, //Lose against Rock, Play Scissors (3)
        { ("A", "Y"), 4 }, //Draw against Rock, Play Rock (3 + 1)
        { ("A", "Z"), 8 }, //Win against Rock, Play Paper (6 + 2)
        { ("B", "X"), 1 }, //Lose against Paper, Play Rock (1)
        { ("B", "Y"), 5 }, //Draw against Paper, Play Paper (3 + 2)
        { ("B", "Z"), 9 }, //Win against Paper, Play Scissors (6 + 3)
        { ("C", "X"), 2 }, //Lost against Scissors, Play Paper (2)
        { ("C", "Y"), 6 }, //Draw against Scissors, Play Scissors (3 + 3)
        { ("C", "Z"), 7 }, //Win against Scissors, Play Rock
    };
    public override string Part1()
    {
        var input = File.ReadAllLines("input.txt");
        var score = 0;

        foreach (var line in input)
        {
            var parts = line.Split(' ');
            score += ScoreRound(parts[0], parts[1]);
        }
        return $"{score}";
    }

    private static int ScoreRound(string opp, string play, bool strategize = false)
    {
        return (strategize? StrategyTable:ScoreTable).TryGetValue((opp,play), out var score) 
            ? score 
            : throw new ArgumentException($"Invalid Inputs: ({opp},{play})");
    }

    public override string Part2()
    {
        var input = File.ReadAllLines("input.txt");
        var score = 0;

        foreach (var line in input)
        {
            var parts = line.Split(' ');
            score += ScoreRound(parts[0], parts[1], true);
        }
        return $"{score}";
    }
}

