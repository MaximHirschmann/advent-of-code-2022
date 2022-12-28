namespace Day17
{
    class Problem2
    {
        // solve day 17 of advent of code 2022
        public static void Solve()
        {
            string input = Solution.ReadInput();

            long ITERATIONS = 1_000_000_000_000;

            Chamber chamber = new Chamber(input);

            (int heightDiff, int idxDiff, int startHeight, int startIdx, int jetsIndex, string top, int type) = chamber.GetRepeating();
            
            Chamber chamber2 = new Chamber(input, jetsIndex, top);

            int tempHeight = chamber2.currentHighest;
            
            for (int i = 0; i < (ITERATIONS - startIdx) % idxDiff; i++)
            {
                chamber2.DropBlock(type % 5);
                type++;
            }

            long res = startHeight
                + (ITERATIONS - startIdx) / idxDiff * heightDiff
                + chamber2.currentHighest - tempHeight;
            
            Console.WriteLine(res);
        }
    }
}