using System;

namespace Day8
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
            string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day8\\input.txt");

            return lines;
        }

        public static int[][] GetGrid()
        {
            string[] input = ReadInput();

            int[][] grid = new int[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                grid[i] = input[i].Select(s => Convert.ToInt32(s) - 48).ToArray();
            }

            return grid;
        }
    }
}