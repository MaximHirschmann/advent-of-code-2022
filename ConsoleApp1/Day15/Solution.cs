using System;

namespace Day15
{
    public class Solution
    {
        public static void Solve()
        {
            Problem1.Solve();
            Problem2.Solve();
        }

        public static string ReadInput()
        {
            string lines = File.ReadAllText(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day15\\input.txt");

            return lines;
        }
    }
}