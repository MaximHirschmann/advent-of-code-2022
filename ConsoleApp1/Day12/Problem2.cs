namespace Day12
{
    class Problem2
    {
        public static void Solve()
        {
            Solution sol = new Solution();

            int[,] grid = sol.GetGrid();
            (int start_y, int start_x) = sol.end;

            List<(int, int)> directions = new() { (0, 1), (1, 0), (0, -1), (-1, 0) };
            int[,] visited = new int[grid.GetLength(0), grid.GetLength(1)];
            visited[start_y, start_x] = 1;

            // bfs
            Stack<(int, int, int, int)> stack = new();
            stack.Push((0, 26, start_y, start_x));

            while (stack.Count > 0)
            {
                Stack<(int, int, int, int)> newStack = new();
                foreach ((int depth, int curr_value, int curr_y, int curr_x) in stack)
                {
                    foreach ((int dy, int dx) in directions)
                    {
                        (int new_y, int new_x) = (curr_y + dy, curr_x + dx);
                        if (new_y < 0 || new_y >= grid.GetLength(0) || new_x < 0 || new_x >= grid.GetLength(1))
                        {
                            continue;
                        }
                        int new_value = grid[new_y, new_x];

                        if (visited[new_y, new_x] == 0 && (curr_value - new_value) < 2)
                        {
                            if (new_value == 0)
                            {
                                Console.WriteLine(depth + 1);
                                return;
                            }

                            newStack.Push((depth + 1, new_value, new_y, new_x));
                            visited[new_y, new_x] = depth + 1;
                        }
                    }
                }
                stack = newStack;
            }
        }
    }
}