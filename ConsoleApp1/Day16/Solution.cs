using System;
using System.Diagnostics;

namespace Day16
{
    public class Solution
    {
        public static void Solve()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Problem1.Solve();
            Console.WriteLine(sw.Elapsed.ToString());

            Stopwatch sw3 = Stopwatch.StartNew();
            //Problem1Graph.Solve();
            Console.WriteLine(sw3.Elapsed.ToString());

            Stopwatch sw2 = Stopwatch.StartNew();
            Problem2.Solve();
            Console.WriteLine(sw2.Elapsed.ToString());
        }

        public static string ReadInput()
        {
            string thisFIlePath = new System.Diagnostics.StackTrace(true).GetFrame(0)!.GetFileName()!;
            string directoryPath = System.IO.Path.GetDirectoryName(thisFIlePath)!;
            string inputTxtPath = directoryPath + @"\input.txt";

            string lines = File.ReadAllText(inputTxtPath);

            return lines;
        }
    }
}