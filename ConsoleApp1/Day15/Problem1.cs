using System.Text.RegularExpressions;

namespace Day15
{
    class Problem1
    {
        public static void Solve()
        {
            string input = Solution.ReadInput();

            string pattern = @"Sensor at x=(-?[0-9]*), y=(-?[0-9]*): closest beacon is at x=(-?[0-9]*), y=(-?[0-9]*)";

            int row = 2_000_000;

            List<(int, int)> ranges = new();

            foreach (Match m in Regex.Matches(input, pattern))
            {
                int sensor_x = Convert.ToInt32(m.Groups[1].ToString());
                int sensor_y = Convert.ToInt32(m.Groups[2].ToString());
                int beacon_x = Convert.ToInt32(m.Groups[3].ToString());
                int beacon_y = Convert.ToInt32(m.Groups[4].ToString());

                int distanceToRow = Math.Abs(row - sensor_y);
                int distanceToBeacon = Math.Abs(sensor_x - beacon_x) + Math.Abs(sensor_y - beacon_y);

                if (distanceToRow <= distanceToBeacon)
                {
                    ranges.Add((
                        sensor_x  - (distanceToBeacon - distanceToRow),
                        sensor_x  + (distanceToBeacon - distanceToRow) + 1
                        ));
                }
            }

            List<(int, int)> newRanges = SimplifyRanges(ranges);
            
            int res = 0;
            foreach ((int start, int end) in newRanges)
            {
                res += end - start - 1;
            }

            Console.WriteLine(res);
        }

        public static List<(int, int)> SimplifyRanges(List<(int, int)> ranges)
        {
            if (ranges.Count == 0)
            {
                return ranges;
            }
            ranges.Sort();

            List<(int, int)> newRanges = new();

            (int last_start, int last_end) = ranges[0];
            for (int i = 1; i < ranges.Count; i++)
            {
                (int curr_start, int curr_end) = ranges[i];
                if (curr_start <= last_end)
                {
                    last_end = int.Max(last_end, curr_end);
                }
                else
                {
                    newRanges.Add((last_start, last_end));
                    (last_start, last_end) = (curr_start, curr_end);
                }
            }
            newRanges.Add((last_start, last_end));

            return newRanges;
        }
    }
}