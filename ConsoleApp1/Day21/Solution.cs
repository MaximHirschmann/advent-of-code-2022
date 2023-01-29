using System;

namespace Day21;

public class Solution
{
    public static void Solve()
    {

        Problem1.Solve();
        Problem2.Solve();
    }

    public static string[] ReadInput()
    {
        string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day21\\input.txt");

        return lines;
    }
}

public class Monkey
{
    public string name;
    public long? number;
    public Operation? operation;

    public string calculation;

    public Monkey(string line)
    {
        if (new[] { '+', '-', '/', '*' }.Any(c => line.Contains(c)))
        {
            // is operation monkey
            string[] parts = line.Replace(":", "").Split(" ");
            this.name = parts[0];
            this.number = null;
            this.operation = new Operation(parts[1], parts[3], parts[2].ElementAt(0));
            this.calculation = "";
        }
        else
        {
            // number monkey
            string[] parts = line.Replace(":", "").Split(" ");
            this.name = parts[0];
            this.number = long.Parse(parts[1]);
            this.operation = null;
            this.calculation = this.name == "humn" ? "humn" : parts[1];
        }
    }
}

public class Operation
{
    public string monkey1;
    public string monkey2;
    public char operation;

    public Operation(string monkey1, string monkey2, char operation)
    {
        this.monkey1 = monkey1;
        this.monkey2 = monkey2;
        this.operation = operation;
    }
}