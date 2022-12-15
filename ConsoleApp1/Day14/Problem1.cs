namespace Day14
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Grid grid = new Grid();

            grid.AddWalls(4, 498, 6, 498);
            grid.AddWalls(6, 498, 6, 496);

            grid.AddWalls(4, 503, 4, 502);
            grid.AddWalls(4, 502, 9, 502);
            grid.AddWalls(9, 502, 9, 494);

            grid.Print();

            int count = 0;
            while (true)
            {
                if (!grid.DropSand())
                {
                    break;
                }
                count++;
            }
            grid.Print();
            Console.WriteLine(count);

        }
    }
}