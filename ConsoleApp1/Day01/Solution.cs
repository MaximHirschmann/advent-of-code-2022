using System;

namespace Day1 {
    public class Solution
    {
        public static void Solve()
        {
            Day1.Problem1.Solve();
            Day1.Problem2.Solve();
        }

        public static string[] ReadInput()
        {
            string thisFIlePath = new System.Diagnostics.StackTrace(true).GetFrame(0)!.GetFileName()!;
            string directoryPath = System.IO.Path.GetDirectoryName(thisFIlePath)!;
            string inputTxtPath = directoryPath + @"\input.txt";

            string[] lines = File.ReadAllLines(inputTxtPath);

            return lines;
        }

        public static List<int> Aggregate(string[] lines)
        {
            List<int> elfs = new() { 0 };

            foreach (var line in lines)
            {
                if (line == "")
                {
                    elfs.Add(0);
                }
                else
                {
                    elfs[^1] += int.Parse(line);
                }
            }

            return elfs;
        }
    }
}

