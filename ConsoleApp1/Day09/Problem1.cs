namespace Day9
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Dictionary<char, (int, int)> directions = new()
            {
                {'U', (1, 0) },
                {'D', (-1, 0)},
                {'R', (0, 1) },
                {'L', (0, -1)}
            };

            HashSet<(int, int)> positions = new();
            positions.Add((0, 0));

            int tail_x = 0;
            int tail_y = 0;
            int head_x = 0;
            int head_y = 0;

            foreach (string line in input)
            {
                char direction = line[0];
                (int d_y, int d_x) = directions[direction];

                int steps = int.Parse(line.Split(' ')[1]);

                for (int i = 0; i < steps; i++)
                {
                    head_y += d_y;
                    head_x += d_x;

                    if (head_y - tail_y > 1)
                    {
                        tail_y += 1;
                        tail_x = head_x;
                    } 
                    else if (head_y - tail_y < -1)
                    {
                        tail_y -= 1;
                        tail_x = head_x;
                    }
                    else if (head_x - tail_x > 1)
                    {
                        tail_y = head_y;
                        tail_x += 1;
                    }
                    else if (head_x - tail_x < -1)
                    {
                        tail_y = head_y;
                        tail_x -= 1;
                    }

                    if (!positions.Contains((tail_y, tail_x)))
                    {
                        positions.Add((tail_y, tail_x));
                    }
                }

            }
            
            Console.WriteLine(positions.Count);
        }
    }
}