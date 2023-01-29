using System;

namespace Day19
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

        public static List<Mine> FilterMines(List<Mine> mines)
        {
            int CUT = 50_000;

            if (mines.Count < CUT)
            {
                return mines;
            }

            return mines
                .OrderByDescending(mine => mine.geode)
                .Take(CUT)
                .ToList();
        }
    }

    public class Blueprint
    {
        public int oreOre;
        public int clayOre;
        public int obsidianOre;
        public int obsidianClay;
        public int geodeOre;
        public int geodeObsidian;

        public Blueprint(int oreOre, int clayOre, int obsidianOre, int obsidianClay, int geodeOre, int geodeObsidian)
        {
            this.oreOre = oreOre;
            this.clayOre = clayOre;
            this.obsidianOre = obsidianOre;
            this.obsidianClay = obsidianClay;
            this.geodeOre = geodeOre;
            this.geodeObsidian = geodeObsidian;
        }
    }

    public enum RobotType { Ore, Clay, Obsidian, Geode }

    public class Mine
    {
        public Blueprint bp;

        public int ore = 0;
        public int clay = 0;
        public int obsidian = 0;
        public int geode = 0;

        public int oreRoboters = 1;
        public int clayRoboters = 0;
        public int obsidianRoboters = 0;
        public int geodeRoboters = 0;

        public int minutes = 0;

        public RobotType? toBeBuild = null;

        HashSet<RobotType> newOptionsToBuild = new();

        public Mine(Blueprint bp)
        {
            this.bp = bp;
        }

        public Mine(Mine mine)
        {
            this.bp = mine.bp;

            this.ore = mine.ore;
            this.clay = mine.clay;
            this.obsidian = mine.obsidian;
            this.geode = mine.geode;

            this.oreRoboters = mine.oreRoboters;
            this.clayRoboters = mine.clayRoboters;
            this.obsidianRoboters = mine.obsidianRoboters;
            this.geodeRoboters = mine.geodeRoboters;

            this.minutes = mine.minutes;

            this.newOptionsToBuild = mine.newOptionsToBuild;
        }

        public void Next()
        {
            HashSet<RobotType> previousOptions = this.GetOptionsToBuild();

            ore += oreRoboters;
            clay += clayRoboters;
            obsidian += obsidianRoboters;
            geode += geodeRoboters;

            minutes += 1;

            HashSet<RobotType> optionsNow = this.GetOptionsToBuild();
            optionsNow.ExceptWith(previousOptions);
            
            this.newOptionsToBuild = optionsNow;
        }

        public HashSet<RobotType> GetOptionsToBuild()
        {
            return Enum.GetValues(typeof(RobotType)).Cast<RobotType>()
                .Where(this.CanBuildRoboter)
                .ToHashSet();
        }
        
        public List<Mine> BuildNewRoboters()
        {
            // if you can build a geode roboter, build it
            if (this.newOptionsToBuild.Contains(RobotType.Geode))
            {
                Mine geodeMine = new Mine(this);
                geodeMine.ScheduleRoboter(RobotType.Geode);
                return new List<Mine>() { geodeMine };
            }
            
            if (this.newOptionsToBuild.Count == 0)
            {
                // more materials won't give you anything
                if (this.clayRoboters == 0 && this.obsidianRoboters == 0 && 
                    this.CanBuildRoboter(RobotType.Ore) && this.CanBuildRoboter(RobotType.Clay))
                {
                    return new List<Mine>();
                }

                if (this.obsidianRoboters == 0 && 
                    this.CanBuildRoboter(RobotType.Obsidian) && this.CanBuildRoboter(RobotType.Ore) && this.CanBuildRoboter(RobotType.Clay))
                {
                    return new List<Mine>();
                }
            }
            
            List<Mine> mines = new() { this };

            foreach (RobotType robotType in this.newOptionsToBuild)
            {
                Mine newMine = new(this);
                newMine.ScheduleRoboter(robotType);
                mines.Add(newMine);
            }

            return mines;
        }

        public void ScheduleRoboter(RobotType robotType)
        {
            this.toBeBuild = robotType;
            switch (robotType)
            {
                case RobotType.Ore:
                    this.ore -= this.bp.oreOre;
                    break;
                case RobotType.Clay:
                    this.ore -= this.bp.clayOre;
                    break;
                case RobotType.Obsidian:
                    this.ore -= this.bp.obsidianOre;
                    this.clay -= this.bp.obsidianClay;
                    break;
                case RobotType.Geode:
                    this.ore -= this.bp.geodeOre;
                    this.obsidian -= this.bp.geodeObsidian;
                    break;
                default:
                    throw new ArgumentException("Invalid robot type");
            }
        }

        public void FinishRoboter()
        {
            switch (this.toBeBuild)
            {
                case null:
                    break;
                case RobotType.Ore:
                    this.oreRoboters += 1;
                    break;
                case RobotType.Clay:
                    this.clayRoboters += 1;
                    break;
                case RobotType.Obsidian:
                    this.obsidianRoboters += 1;
                    break;
                case RobotType.Geode:
                    this.geodeRoboters += 1;
                    break;
                default:
                    throw new ArgumentException("Invalid robot type");
            }

            this.toBeBuild = null;
        }
        public bool CanBuildRoboter(RobotType robotType)
        {
            switch (robotType)
            {
                case RobotType.Ore:
                    return ore >= bp.oreOre;
                case RobotType.Clay:
                    return ore >= bp.clayOre;
                case RobotType.Obsidian:
                    return ore >= bp.obsidianOre && clay >= bp.obsidianClay;
                case RobotType.Geode:
                    return ore >= bp.geodeOre && obsidian >= bp.geodeObsidian;
                default:
                    throw new ArgumentException("Invalid robot type");
            }
        }
    }
}