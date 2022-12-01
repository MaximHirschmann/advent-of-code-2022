class Problem2
{
    public static void Solve()
    {
        string[] input = Solution.ReadInput();

        List<int> elfs = Solution.Aggregate(input);

        // O(n)
        int res = 0;
        for (int i = 0; i < 3; i++)
        {
            int maxVal = elfs.Max();
            elfs.Remove(maxVal);
            res += maxVal;
        }

        Console.WriteLine(res);
    }
}