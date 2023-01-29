namespace Day22;

class Problem1
{
    public static void Solve()
    {
        string[] input = Solution.ReadInput();

        Map map = new Map(input);

        List<Command> commands = Solution.ReadInstruction(input[^1]);

        Player player = new Player(map);

        foreach (Command command in commands)
        {
            player.Move(command);
        }

        long res = player.Password();

        Console.WriteLine(res);

        return;
    }
}