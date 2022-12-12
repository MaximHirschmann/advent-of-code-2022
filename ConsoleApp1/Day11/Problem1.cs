namespace Day11
{
    class Problem1
    {
        public static void Solve()
        {
            Pack pack = new Pack();

            pack.DoMultipleRounds(20);
            long res = pack.MonkeyBusiness();

            Console.WriteLine(res);
        }
    }
}