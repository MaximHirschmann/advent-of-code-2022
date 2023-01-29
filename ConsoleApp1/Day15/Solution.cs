﻿using System;

namespace Day15
{
    public class Solution
    {
        public static void Solve()
        {
            Problem1.Solve();
            Problem2.Solve();
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