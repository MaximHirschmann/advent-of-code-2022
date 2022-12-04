namespace Day1
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            List<int> elfs = Solution.Aggregate(input);

            var res = elfs.Max();
            Console.WriteLine(res);
        }
    }
}