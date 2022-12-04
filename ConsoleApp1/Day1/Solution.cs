using System;

namespace Day1 {
    public class Solution
    {
        public static void Solve()
        {
            Day1.Problem1.Solve();
            Day1.Problem2.Solve();
        }

        public static string[] ReadInput()
        {
            string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day1\\input.txt");

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
}

