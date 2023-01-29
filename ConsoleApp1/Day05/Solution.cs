using System;

namespace Day5
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

    public struct Move
    {
        public int crates;
        public int from;
        public int to;

        public Move(int crates, int from, int to)
        {
            this.crates = crates;
            this.from = from;
            this.to = to;
        }
    }

    public class Ship
    {
        private List<Stack<char>> stacks;

        public Ship() { 
            this.stacks = new List<Stack<char>>()
            {
                new Stack<char>(new[] {'W', 'R', 'F'}),
                new Stack<char>(new[] {'T', 'H', 'M', 'C', 'D', 'V', 'W', 'P'}),
                new Stack<char>(new[] {'P', 'M', 'Z', 'N', 'L'}),
                new Stack<char>(new[] {'J', 'C', 'H', 'R'}),
                new Stack<char>(new[] {'C', 'P', 'G', 'H', 'Q', 'T', 'B'}),
                new Stack<char>(new[] {'G', 'C', 'W', 'L', 'F', 'Z'}),
                new Stack<char>(new[] {'W', 'V', 'L', 'Q', 'Z', 'J', 'G', 'C'}),
                new Stack<char>(new[] {'P', 'N', 'R', 'F', 'W', 'T', 'V', 'C'}),
                new Stack<char>(new[] {'J', 'W', 'H', 'G', 'R', 'S', 'V'}),
            };
        }

        public void DoMove(Move move)
        {
            for (int i = 0; i < move.crates; i++)
            {
                char elem = this.stacks[move.from - 1].Pop();
                this.stacks[move.to - 1].Push(elem);
            }
        }

        public void DoMove2(Move move)
        {
            Stack<char> stack = new();
            for (int i = 0; i < move.crates; i++)
            {
                stack.Push(this.stacks[move.from - 1].Pop());
            }
            for (int i = 0; i < move.crates; i++)
            {
                this.stacks[move.to - 1].Push(stack.Pop());
            }
        }

        public string GetTopLine()
        {
            string res = "";
            foreach (var stack in this.stacks)
            {
                res += stack.Peek();
            }
            return res;
        }
    }
}