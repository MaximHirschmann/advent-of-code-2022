namespace Day3
{
    class Problem1
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            int res = 0;
            foreach (string s in input)
            {
                string first = s.Substring(0, (s.Length / 2));
                string second = s.Substring((s.Length / 2));

                HashSet<char> occured = new();

                foreach (char c in first)
                {
                    occured.Add(c);
                }

                foreach (char c in second)
                {
                    if (occured.Contains(c))
                    {
                        res += Priority(c);
                        break;
                    }
                }

            }

            Console.WriteLine(res);
        }

        public static int Priority(char c)
        {
            if (Char.IsUpper(c))
            {
                return (int)c - (int)'A' + 27;
            }
            else
            {
                return (int)c - (int)'a' + 1;
            }
        }
    }
}