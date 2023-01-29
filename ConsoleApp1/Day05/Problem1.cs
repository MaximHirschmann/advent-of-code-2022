namespace Day5
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Ship ship = new Ship();

            foreach (string line in input)
            {
                if (!line.Contains("move"))
                {
                    continue;
                }
                string numeric = new string(line.Where(c => char.IsDigit(c) || c == ' ').ToArray()).Trim();
                string[] values = numeric.Split("  ");
                Move move = new Move(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
                ship.DoMove(move);

            }
            Console.WriteLine(ship.GetTopLine());
        }
    }
}