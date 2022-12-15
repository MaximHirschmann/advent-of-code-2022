namespace Day14
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Grid grid = new Grid();
            foreach (string s in input)
            {
                string[] split = s.Split(" -> ");
                int x = Convert.ToInt32(split[0].Split(',')[0]);
                int y = Convert.ToInt32(split[0].Split(',')[1]);
                for (int i = 1; i < split.Length; i++)
                {
                    int x2 = Convert.ToInt32(split[i].Split(',')[0]);
                    int y2 = Convert.ToInt32(split[i].Split(',')[1]);

                    grid.AddWalls(y, x, y2, x2);

                    x = x2;
                    y = y2;
                }
            }

            int count = 1;
            while (true)
            {
                if (!grid.DropSand2())
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