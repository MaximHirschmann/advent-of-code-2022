using System;
using System.Security.Cryptography.X509Certificates;

namespace Day14
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
            string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day14\\input.txt");

            return lines;
        }
    }

    public class Grid
    {
        private Dictionary<(int, int), char> blocked;
        private int lowestBlock;

        public Grid()
        {
            this.blocked = new();
            this.lowestBlock = 0;
        }

        private void AddBlock(int y, int x, char character = '#')
        {
            this.blocked[(y, x)] = character;
            if (y > this.lowestBlock) {
                this.lowestBlock = y;
            }
        }

        public void AddWalls(int y1, int x1, int y2, int x2)
        {
            AddBlock(y2, x2);
            if (x1 == x2)
            {
                int y = y1;
                while (y != y2)
                {
                    AddBlock(y, x1);

                    if (y1 < y2) y++;
                    else y--;
                }
            }
            else if (y1 == y2)
            {
                int x = x1;
                while (x != x2)
                {
                    AddBlock(y1, x);

                    if (x1 < x2) x++;
                    else x--;
                }
            }
            else
            {
                throw new Exception("Points have to be either on the same horizontal line or on the same vertival line");
            }
        }

        public bool DropSand()
        {
            (int y, int x) = (0, 500);

            while (true)
            {
                (int, int) below = (y + 1, x);
                (int, int) leftBelow = (y + 1, x + 1);
                (int, int) rightBelow = (y + 1, x - 1);

                if (!this.blocked.ContainsKey(below))
                {
                    (y, x) = below;
                }
                else if (!this.blocked.ContainsKey(leftBelow))
                {
                    (y, x) = leftBelow;
                }
                else if (!this.blocked.ContainsKey(rightBelow))
                {
                    (y, x) = rightBelow;
                }
                else
                {
                    AddBlock(y, x, 'o');
                    return true;
                }

                if (y > this.lowestBlock)
                {
                    return false;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine();

            int maxY = this.blocked.Keys.Select(k => k.Item1).Max();
            int minY = 0;
            int maxX = this.blocked.Keys.Select(k => k.Item2).Max();
            int minX = this.blocked.Keys.Select(k => k.Item2).Min();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (this.blocked.ContainsKey((y, x)))
                    {
                        Console.Write(this.blocked[(y, x)]);
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }
    }
}