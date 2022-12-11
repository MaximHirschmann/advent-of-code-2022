namespace Day9
{
    class Problem2
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

            Dictionary<(int, int), (int, int)> physics = new()
            {
                {(0, 2), (0, 1)},
                {(1, 2), (1, 1)},
                {(2, 2), (1, 1)},
                {(2, 1), (1, 1)},
                {(2, 0), (1, 0)},
                {(2, -1), (1, -1)},
                {(2, -2), (1, -1)},
                {(1, -2), (1, -1)},
                {(0, -2), (0, -1)},
                {(-1, -2), (-1, -1)},
                {(-2, -2), (-1, -1)},
                {(-2, -1), (-1, -1)},
                {(-2, 0), (-1, 0)},
                {(-2, 1), (-1, 1)},
                {(-2, 2), (-1, 1)},
                {(-1, 2), (-1, 1)}
            };

            HashSet<(int, int)> positions = new();
            positions.Add((0, 0));

            int ropeLength = 10;

            List<(int, int)> rope = new();
            for (int i = 0; i < ropeLength; i++)
            {
                rope.Add((0, 0));
            }

            foreach (string line in input)
            {
                (int d_y, int d_x) = directions[line[0]];
                int steps = int.Parse(line.Split(' ')[1]);

                for (int i = 0; i < steps; i++)
                {
                    rope[0] = (rope[0].Item1 + d_y, rope[0].Item2 + d_x);

                    for (int j = 1; j < ropeLength; j++)
                    {
                        (int, int) distance = (rope[j - 1].Item1 - rope[j].Item1, rope[j - 1].Item2 - rope[j].Item2);

                        (int delta_y, int delta_x) = physics.ContainsKey(distance) ? physics[distance] : (0, 0);

                        rope[j] = (rope[j].Item1 + delta_y, rope[j].Item2 + delta_x);
                    }

                    positions.Add(rope[^1]);
                }
            }

            Console.WriteLine(positions.Count);
        }
    }
}