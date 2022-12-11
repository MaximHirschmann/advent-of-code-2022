using System.Data.Common;

namespace Day8
{
    class Problem1
    {
        public static void Solve()
        {
            int[][] grid = Solution.GetGrid();

            int n = grid.Length;

            int[,] visible = new int[n, n];

            int leftMax, rightMax, topMax, bottomMax;
            leftMax = rightMax = topMax = bottomMax = -1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Helper(ref grid, ref visible, ref leftMax, i, j);
                    Helper(ref grid, ref visible, ref rightMax, i, n - j - 1);
                    Helper(ref grid, ref visible, ref topMax, j, i);
                    Helper(ref grid, ref visible, ref bottomMax, n - j - 1, i);
                }
                leftMax = rightMax = topMax = bottomMax = -1;
            }

            Console.WriteLine(visible.Cast<int>().Sum());

            return;
        }

        public static void Helper(ref int[][] grid, ref int[,] visible, ref int max, int row, int col)
        {
            if (grid[row][col] > max)
            {
                max = grid[row][col];
                visible[row, col] = 1;
            }
        }
    }
}