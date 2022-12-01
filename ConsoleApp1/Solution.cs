using System;

public class Solution
{
	public static void Main()
	{
        Problem1.Solve();
        Problem2.Solve();
	}

    public static string[] ReadInput()
    {
        string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\input.txt");

        return lines;
    }

    public static List<int> Aggregate(string[] lines)
    {
        List<int> elfs = new() { 0 };

        foreach (var line in lines)
        {
            if (line == "")
            {
                elfs.Add(0);
            }
            else
            {
                elfs[^1] += int.Parse(line);
            }
        }

        return elfs;
    }
}
