
namespace Day21;

class Problem1
{
    public static void Solve()
    {
        string[] input = Solution.ReadInput();

        List<Monkey> monkeys = input.Select(line => new Monkey(line)).ToList();
        Dictionary<string, Monkey> getMonkeyWithName = monkeys.ToDictionary(m => m.name);

        long CalculateMonkey(string name)
        {
            Monkey monkey = getMonkeyWithName[name];

            if (monkey.number != null) return (int) monkey.number;

            long n1 = CalculateMonkey(monkey.operation!.monkey1);
            long n2 = CalculateMonkey(monkey.operation!.monkey2);
            string c1 = getMonkeyWithName[monkey.operation!.monkey1].calculation;
            string c2 = getMonkeyWithName[monkey.operation!.monkey2].calculation;

            switch (monkey.operation!.operation)
            {
                case '+':
                    monkey.number = n1 + n2;
                    monkey.calculation = "(" + c1 + " + " + c2 + ")";
                    return (long) monkey.number;
                case '-':
                    monkey.number = n1 - n2;
                    monkey.calculation = "(" + c1 + " - " + c2 + ")";
                    return (long)monkey.number;
                case '*':
                    monkey.number = n1 * n2;
                    monkey.calculation = "(" + c1 + " * " + c2 + ")";
                    return (long)monkey.number;
                case '/':
                    monkey.number = n1 / n2;
                    monkey.calculation = "(" + c1 + " / " + c2 + ")";
                    return (long)monkey.number;
                default:
                    throw new ArgumentException("unknown operation");
            }
        }

        long res = CalculateMonkey("root");
        string calc1 = getMonkeyWithName[getMonkeyWithName["root"].operation!.monkey1].calculation;
        string calc2 = getMonkeyWithName[getMonkeyWithName["root"].operation!.monkey2].calculation;

        Console.WriteLine(res);
        Console.WriteLine(calc1);
        Console.WriteLine(calc2);

        // further evaluating with python
    }

    
}

