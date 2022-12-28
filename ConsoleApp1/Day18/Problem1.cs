namespace Day18
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            List<(int, int, int)> neighbors = new()
            {
                (0, 0, 1),
                (0, 0, -1),
                (0, 1, 0),
                (0, -1, 0),
                (1, 0, 0),
                (-1, 0, 0)
            };
            
            int result = 0;
            
            HashSet<(int, int, int)> points = new();

            foreach (string line in input)
            {
                string[] parts = line.Split(",");

                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                int z = int.Parse(parts[2]);

                points.Add((x, y, z));
                result += 6;

                foreach ((int dx, int dy, int dz) in neighbors)
                {
                    if (points.Contains((x + dx, y + dy, z + dz)))
                    {
                        result -= 2;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}