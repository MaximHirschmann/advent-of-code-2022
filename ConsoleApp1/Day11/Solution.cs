using System;

namespace Day11
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
            string[] lines = File.ReadAllLines(@"C:\\Users\\maxim\\Documents\\Git\\advent-of-code-2022\\ConsoleApp1\\Day1\\input.txt");

            return lines;
        }
    }

    public class Pack
    {
        List<Monkey> monkeys;
        public Pack()
        {
            this.monkeys = new List<Monkey>()
            {
                new Monkey(
                    new long[] {92, 73, 86, 83, 65, 51, 55, 93},
                    (long old) => { return old * 5; },
                    (long worry) => {return worry % 11 == 0 ? 3 : 4;}
                ),
                new Monkey(
                    new long[] {99, 67, 62, 61, 59, 98},
                    (long old) => { return old * old; },
                    (long worry) => {return worry % 2 == 0 ? 6 : 7;}
                ),
                new Monkey(
                    new long[] {81, 89, 56, 61, 99},
                    (long old) => { return old * 7; },
                    (long worry) => {return worry % 5 == 0 ? 1 : 5;}
                ),
                new Monkey(
                    new long[] {97, 74, 68},
                    (long old) => { return old + 1; },
                    (long worry) => {return worry % 17 == 0 ? 2 : 5;}
                ),
                new Monkey(
                    new long[] {78, 73},
                    (long old) => { return old + 3; },
                    (long worry) => {return worry % 19 == 0 ? 2 : 3;}
                ),
                new Monkey(
                    new long[] {50},
                    (long old) => { return old + 5; },
                    (long worry) => {return worry % 7 == 0 ? 1 : 6;}
                ),
                new Monkey(
                    new long[] {95, 88, 53, 75},
                    (long old) => { return old + 8; },
                    (long worry) => {return worry % 3 == 0 ? 0 : 7;}
                ),
                new Monkey(
                    new long[] {50, 77, 98, 85, 94, 56, 89},
                    (long old) => { return old + 2; },
                    (long worry) => {return worry % 13 == 0 ? 4 : 0;}
                )
            };
            
            /*
            this.monkeys = new List<Monkey>()
            {
                new Monkey(
                    new int[] {79, 98}, 
                    (int old) => { return old * 19; }, 
                    (int worry) => {return worry % 23 == 0 ? 2 : 3;}
                ),
                new Monkey(
                    new int[] {54, 65, 75, 74},
                    (int old) => { return old + 6; },
                    (int worry) => {return worry % 19 == 0 ? 2 : 0;}
                ),
                new Monkey(
                    new int[] {79, 60, 97},
                    (int old) => { return old * old; },
                    (int worry) => {return worry % 13 == 0 ? 1 : 3;}
                ),
                new Monkey(
                    new int[] {74},
                    (int old) => { return old + 3; },
                    (int worry) => {return worry % 17 == 0 ? 0 : 1;}
                ),
            };
            */
        }
    
        public void DoRound(bool divideByThree)
        {
            foreach (Monkey monkey in this.monkeys)
            {
                List<(long, int)> throwingTo = monkey.InspectAllItems(divideByThree);
                foreach ((long worry, int newMonkey) in throwingTo)
                {
                    this.monkeys[newMonkey].AddItem(worry);
                }
            }
        }

        public void DoMultipleRounds(int n, bool divideByThree = true)
        {
            for (int i = 0; i < n; i++)
            {
                this.DoRound(divideByThree);
            }
        }

        public long MonkeyBusiness()
        {
            List<long> ordered = this.monkeys.Select(m => m.inspections).Order().ToList();
            return ordered[^1] * ordered[^2];
        }
    }

    public class Monkey
    {
        Queue<long> items;
        Func<long, long> operation;
        Func<long, int> next;
        public long inspections;

        public Monkey(IEnumerable<long> items, Func<long, long> operation, Func<long, int> next)
        {
            this.items = new Queue<long>(items.Reverse());
            this.operation = operation;
            this.next = next;
            this.inspections = 0;
        }

        internal void AddItem(long worry)
        {
            this.items.Enqueue(worry);
        }

        internal List<(long, int)> InspectAllItems(bool divideByThree)
        {
            List<(long, int)> res = new();

            while (items.Count > 0) { 
                long item = this.items.Dequeue();
                long newWorryLevel;
                if (divideByThree)
                {
                    newWorryLevel = operation(item) / 3;
                }
                else
                {
                    long leastCommonMultiple = 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19;
                    newWorryLevel = operation(item) % leastCommonMultiple;
                }
                res.Add((newWorryLevel, next(newWorryLevel)));
                this.inspections++;
            }

            return res;
        }
    }
}