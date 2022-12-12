namespace Day10
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            int m = 40;
            int cycle = 0;
            int X = 1;

            void Draw()
            {
                if (cycle != 0 && cycle % m == 0)
                {
                    Console.WriteLine();
                }
                if (Math.Abs((cycle % m) - X) < 2) {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }

            Draw();
            foreach (string line in input)
            {
                if (line.Contains("noop"))
                {
                    cycle++;
                    Draw();
                }
                else
                {
                    int add = int.Parse(line.Split(" ")[1]);
                    cycle++;
                    Draw();
                    cycle++;
                    X += add;
                    Draw();
                }
            }
        }
    }
}