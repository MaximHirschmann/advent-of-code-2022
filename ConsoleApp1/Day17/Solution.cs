using System;
using System.Diagnostics;

namespace Day17
{
    public class Solution
    {
        public static void Solve()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Problem1.Solve();
            Console.WriteLine(sw.Elapsed.ToString());

            Stopwatch sw2 = Stopwatch.StartNew();
            Problem2.Solve();
            Console.WriteLine(sw2.Elapsed.ToString());
        }

        public static string ReadInput()
        {
            string lines = File.ReadAllText(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day17\\input.txt");

            return lines;
        }
    }
}