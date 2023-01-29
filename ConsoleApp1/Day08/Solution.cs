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
            string thisFIlePath = new System.Diagnostics.StackTrace(true).GetFrame(0)!.GetFileName()!;
            string directoryPath = System.IO.Path.GetDirectoryName(thisFIlePath)!;
            string inputTxtPath = directoryPath + @"\input.txt";

            string[] lines = File.ReadAllLines(inputTxtPath);

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