using System;

namespace Day6
{
    public class Solution
    {
        public static void Solve()
        {
            Problem1.Solve();
            Problem2.Solve();
        }

        public static string[] ReadInput()
        {
            string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day6\\input.txt");

            return lines;
        }
    }
}