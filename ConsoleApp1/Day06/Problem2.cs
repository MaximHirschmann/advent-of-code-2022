namespace Day6
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Queue<char> queue = new();
            for (int i = 0; i < input[0].Length; i++)
            {
                char c = input[0][i];
                queue.Enqueue(c);
                if (queue.Count() > 14)
                {
                    queue.Dequeue();

                    if (queue.Distinct().Count() == 14)
                    {
                        Console.WriteLine(i + 1);
                        break;
                    }
                }
            }
        }
    }
}