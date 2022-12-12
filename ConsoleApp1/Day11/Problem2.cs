namespace Day11
{
    class Problem2
    {
        public static void Solve()
        {
            Pack pack = new Pack();

            pack.DoMultipleRounds(10_000, false);
            long res = pack.MonkeyBusiness();

            Console.WriteLine(res);
        }
    }
}