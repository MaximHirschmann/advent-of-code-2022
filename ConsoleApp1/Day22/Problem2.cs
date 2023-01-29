namespace Day22;

class Problem2
{
    public static void Solve()
    {
        string[] input = Solution.ReadInput();


    }
}

public class Map2
{
    public int width;
    public int height;

    public List<Face> faces;
    public char[,] grid;

    public Map2(string[] input)
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

public enum Direction
{
    North,
    East,
    West,
    South
}
