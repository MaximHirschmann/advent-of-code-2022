namespace Day4
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            int count = 0;
            foreach (string s in input)
            {
                string[] split = s.Split(',');
                int start1 = int.Parse(split[0].Split("-")[0]);
                int end1 = int.Parse(split[0].Split("-")[1]);
                int start2 = int.Parse(split[1].Split("-")[0]);
                int end2 = int.Parse(split[1].Split("-")[1]);

                if ((start1 <= start2 && end1 >= end2) || (start2 <= start1 && end2 >= end1))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}