namespace Day8
{
    class Problem2
    {
        public static void Solve()
        {
            int[][] grid = Solution.GetGrid();

            int n = grid.Length;

            var res = grid.SelectMany(s => s).Select((_, index) => ScenicScore(ref grid, n, index)).Max();

            Console.WriteLine(res);
            return;
        }
        public static int ScenicScore(ref int[][] grid, int n, int index)
        {
            int row = index / n;
            int col = index % n;

            int res = 1;
            int count = 0;
            for (int i = row+1; i < n; i++)
            {
                count++;
                if (grid[i][col] >= grid[row][col])
                {
                    break;
                }
            }
            res *= count;

            count = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                count++;
                if (grid[i][col] >= grid[row][col])
                {
                    break;
                }
            }
            res *= count; 
            
            count = 0;
            for (int i = col+1; i < n; i++)
            {
                count++;
                if (grid[row][i] >= grid[row][col])
                {
                    break;
                }
            }
            res *= count; 
            
            count = 0;
            for (int i = col - 1; i >= 0; i--)
            {
                count++;
                if (grid[row][i] >= grid[row][col])
                {
                    break;
                }
            }
            res *= count;

            return res;
        }
    }
}