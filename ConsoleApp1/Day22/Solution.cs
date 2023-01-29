using System;

namespace Day22;


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

    public static List<Command> ReadInstruction(string input)
    {
        List<Command> res = new();
        char rotation = 'N';

        string current = "";
        foreach (char c in input)
        {
            if (c == 'R' || c == 'L')
            {
                res.Add(new Command(rotation, int.Parse(current)));
                rotation = c;
                current = "";
            }
            else
            {
                current += c;
            }
        }
        res.Add(new Command(rotation, int.Parse(current)));
        return res;
    }

    public static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

}

public class Player
{
    public Map map;

    public int y;
    public int x;

    public (int, int) orientation;

    public Player(Map map)
    {
        this.map = map;
        this.y = 0;
        for (int i = 0; i < this.map.width; i++)
        {
            if (this.map.grid[0, i] != ' ')
            {
                this.x = i;
                break;
            }
        }
        this.orientation = (0, 1);
    }

    public void Move(Command command)
    {
        this.Rotate(command.rotation);
        for (int i = 0; i < command.steps; i++)
        {
            int newY = Solution.Mod(this.y + this.orientation.Item1, this.map.height);
            int newX = Solution.Mod(this.x + this.orientation.Item2, this.map.width);

            while (this.map.grid[newY, newX] == ' ')
            {
                newY = Solution.Mod(newY + this.orientation.Item1, this.map.height);
                newX = Solution.Mod(newX + this.orientation.Item2, this.map.width);
            }
            if (this.map.grid[newY, newX] == '#')
            {
                break;
            }
            this.y = newY;
            this.x = newX;
        }
    }


    private void Rotate(char rotation)
    {
        int dy = this.orientation.Item1;
        int dx = this.orientation.Item2;
        if (rotation == 'L')
        {
            this.orientation = (-dx, dy);
        }
        else if (rotation == 'R')
        {
            this.orientation = (dx, -dy);
        }
    }

    public long Password()
    {
        Dictionary<(int, int), int> facing = new()
        {
            {(0, 1), 0 },
            {(1, 0), 1 },
            {(0, -1), 2 },
            {(-1, 0), 3 }
        };
        return 1000 * (this.y + 1) + 4 * (this.x + 1) + facing[this.orientation];
    }
}

public class Map
{
    public int width;
    public int height;

    public char[,] grid;

    public Map(string[] input)
    {
        this.height = input.Length - 2;
        this.width = input.Where(x => !x.Contains('R')).Max(x => x.Length);

        this.grid = new char[this.height, this.width];

        for (int i = 0; i < this.height; i++)
        {
            for (int j = 0; j < this.width; j++)
            {
                if (j >= input[i].Length)
                {
                    this.grid[i, j] = ' ';
                }
                else
                {
                    this.grid[i, j] = input[i][j];
                }
            }
        }
    }
}

public class Command
{
    public char rotation;
    public int steps;

    public Command(char rotation, int steps)
    {
        this.rotation = rotation;
        this.steps = steps;
    }

    public override string ToString()
    {
        return $"{rotation}: {steps}";
    }
}