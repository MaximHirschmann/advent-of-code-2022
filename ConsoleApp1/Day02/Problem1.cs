namespace Day2
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Dictionary<string, int> scores = new()
            {
                {"A X", 4}, // rock against rock 3+1
                {"A Y", 8}, // rock against paper 6+2
                {"A Z", 3}, // rock against scissor 0+3
                {"B X", 1}, // paper against rock 0+1
                {"B Y", 5}, // paper against paper 3+2
                {"B Z", 9}, // paper against scissor 6+3
                {"C X", 7}, // scissor against rock 6+1
                {"C Y", 2}, // scissor against paper 0+2
                {"C Z", 6}, // scissor against scissor 3+3
            };

            int res = 0;
            foreach (string s in input)
            {
                res += scores[s];
            }
            Console.WriteLine(res);
        }
    }
}