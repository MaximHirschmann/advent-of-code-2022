using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day19;
class Problem2
{
    public static void Solve()
    {
        int DEPTH = 32;

        string input = Solution.ReadInput();

        string pattern = @"Blueprint [0-9]:
  Each ore robot costs ([0-9]*) ore.
  Each clay robot costs ([0-9]*) ore.
  Each obsidian robot costs ([0-9]*) ore and ([0-9]*) clay.
  Each geode robot costs ([0-9]*) ore and ([0-9]*) obsidian.";

        string pattern2 = @"Blueprint [0-9]*: Each ore robot costs ([0-9]*) ore. Each clay robot costs ([0-9]*) ore. Each obsidian robot costs ([0-9]*) ore and ([0-9]*) clay. Each geode robot costs ([0-9]*) ore and ([0-9]*) obsidian.";

        Regex regex = new Regex(pattern2);
        int matchCounter = 0;
        int product = 1;

        Stopwatch stopwatch = Stopwatch.StartNew();
        
        foreach (Match match in regex.Matches(input))
        {
            if (matchCounter == 3)
                break;

            matchCounter++;

            int oreOre = int.Parse(match.Groups[1].Value);
            int clayOre = int.Parse(match.Groups[2].Value);
            int obsidianOre = int.Parse(match.Groups[3].Value);
            int obsidianClay = int.Parse(match.Groups[4].Value);
            int geodeOre = int.Parse(match.Groups[5].Value);
            int geodeObsidian = int.Parse(match.Groups[6].Value);

            Blueprint bp = new Blueprint(oreOre, clayOre, obsidianOre, obsidianClay, geodeOre, geodeObsidian);

            Mine start = new Mine(bp);

            int res = 0;
            int count = 0;

            // bfs
            Stopwatch stopwatch1 = Stopwatch.StartNew();
            List<Mine> current = new() { start };

            while (current.Count > 0)
            {
                List<Mine> next = new();

                foreach (Mine mine in current)
                {
                    if (mine.minutes == DEPTH)
                    {
                        if (mine.geode > res)
                        {
                            res = mine.geode;
                            Mine toDebug = current.MaxBy(mine => mine.geode)!;
                            Console.Write("");
                        }
                        continue;
                    }

                    List<Mine> newMines = mine.BuildNewRoboters();
                    foreach (Mine newMine in newMines)
                    {
                        newMine.Next();
                        newMine.FinishRoboter();
                    }
                    next.AddRange(newMines);
                }
                
                current = Solution.FilterMines(next);
                count++;
            }

            Console.WriteLine($"Blueprint: {matchCounter} Result: {res} After: {stopwatch1.ElapsedMilliseconds} ms");
            product *= res;
        }
        Console.WriteLine($"The result for problem2 is {product}. Calculated in {stopwatch.ElapsedMilliseconds} ms");
    }
}