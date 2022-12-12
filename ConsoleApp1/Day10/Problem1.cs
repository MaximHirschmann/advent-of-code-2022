namespace Day10
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            int res = 0;
            int n = 20;
            int m = 40;
            int cycle = 0;
            int X = 1;

            void UpdateRes()
            {
                if ((cycle - n) % m == 0)
                {
                    Console.Write(cycle);
                    Console.WriteLine(" * " + X.ToString());
                    res += cycle * X;
                }
            }

            foreach (string line in input) { 
                if (line.Contains("noop"))
                {
                    cycle++;
                    UpdateRes();
                }
                else
                {
                    int add = int.Parse(line.Split(" ")[1]);
                    cycle++;
                    UpdateRes();
                    cycle++;
                    UpdateRes();
                    X += add;
                }
            }
            
            Console.WriteLine(res);
        }
    }
}