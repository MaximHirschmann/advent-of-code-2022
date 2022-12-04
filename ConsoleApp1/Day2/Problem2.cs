namespace Day2
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Dictionary<string, int> scores = new()
            {
                {"A X", 3}, // rock against scissor 0+3
                {"A Y", 4}, // rock against rock 3+1
                {"A Z", 8}, // rock against paper 6+2
                {"B X", 1}, // paper against rock 0+1
                {"B Y", 5}, // paper against paper 3+2
                {"B Z", 9}, // paper against scissor 6+3
                {"C X", 2}, // scissor against paper 0+2
                {"C Y", 6}, // scissor against scissor 3+3
                {"C Z", 7}, // scissor against rock 6+1
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