using System;

namespace Day12
{
    public class Solution
    {
        public (int, int) start;
        public (int, int) end;

        public static void Solve()
        {
            Problem1.Solve();
            Problem2.Solve();
        }

        public int[,] GetGrid()
        {
            string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day12\\input.txt");

            int[,] grid = new int[lines.Length,lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'S')
                    {
                        this.start = (i, j);
                        grid[i, j] = 0;
                    }
                    else if (lines[i][j] == 'E')
                    {
                        this.end = (i, j);
                        grid[i, j] = 26;
                    }
                    else
                    {
                        grid[i, j] = Convert.ToInt32(lines[i][j]) - Convert.ToInt32('a');
                    }
                }
            }

            return grid;
        }
    }
}