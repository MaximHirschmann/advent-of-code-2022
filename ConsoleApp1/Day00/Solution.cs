using System;

namespace Day0
{
    public class Solution
    {
        public static void Solve()
        {
            Problem1.Solve();
            Problem2.Solve();
        }

        public static string[] ReadInput()
        {
            string thisFIlePath = new System.Diagnostics.StackTrace(true).GetFrame(0)!.GetFileName()!;
            string directoryPath = System.IO.Path.GetDirectoryName(thisFIlePath)!;
            string inputTxtPath = directoryPath + @"\input.txt";

            string[] lines = File.ReadAllLines(inputTxtPath);

            return lines;
        }
    }
}