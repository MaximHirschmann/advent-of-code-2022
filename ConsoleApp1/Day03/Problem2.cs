namespace Day3
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            int res = 0;

            for (int i = 0; i < input.Length; i += 3)
            {
                HashSet<char> set1 = new(input[i]);
                HashSet<char> set2 = new(input[i + 1]);
                HashSet<char> set3 = new(input[i + 2]);

                set1.IntersectWith(set2);
                set1.IntersectWith(set3);

                res += Problem1.Priority(set1.Single());
            }

            Console.WriteLine(res);
        }
    }
}