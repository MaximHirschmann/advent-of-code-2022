using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Day15
{

    class Problem2
    {
        List<Sensor>? sensors;

        public static void Solve()
        {
            Problem2 problem = new Problem2();
            problem.SolveNonStatic();
        }

        public void SolveNonStatic() {
            string input = Solution.ReadInput();

            string pattern = @"Sensor at x=(-?[0-9]*), y=(-?[0-9]*): closest beacon is at x=(-?[0-9]*), y=(-?[0-9]*)";

            List<Diamond> diamonds = new();
            this.sensors = new();

            // read input data
            // construct diamonds and sensors
            foreach (Match m in Regex.Matches(input, pattern))
            {
                long sensor_x = Convert.ToInt32(m.Groups[1].ToString());
                long sensor_y = Convert.ToInt32(m.Groups[2].ToString());
                long beacon_x = Convert.ToInt32(m.Groups[3].ToString());
                long beacon_y = Convert.ToInt32(m.Groups[4].ToString());

                long distanceToBeacon = Math.Abs(sensor_x - beacon_x) + Math.Abs(sensor_y - beacon_y);

                // increase radius of diamond by 1, so that intersections with other diamonds occur
                Pos above = new Pos(sensor_x, sensor_y - distanceToBeacon - 1);
                Pos below = new Pos(sensor_x, sensor_y + distanceToBeacon + 1);
                Pos left = new Pos(sensor_x - distanceToBeacon - 1, sensor_y);
                Pos right = new Pos(sensor_x + distanceToBeacon + 1, sensor_y);

                Sensor sensor = new Sensor(sensor_x, sensor_y, distanceToBeacon);
                this.sensors.Add(sensor);

                Diamond polygon = new Diamond(new List<Pos>() { above, right, below, left });

                diamonds.Add(polygon);
            }

            // count positions where intersections of a lot of diamonds occur
            // these are the points that are just on the border of the radius of multiple sensors
            Dictionary<Pos, int> candidates = new();

            for (int i = 0; i < diamonds.Count; i++)
            {
                for (int j = i+1; j < diamonds.Count; j++)
                {
                    foreach (Pos candidate in GetIntersectionPoints(diamonds[i], diamonds[j]))
                    {
                        if (!candidates.ContainsKey(candidate))
                        {
                            candidates[candidate] = 0;
                        }
                        candidates[candidate]++;
                    }
                }
            }

            // one of the candidates (probably the pos with the most intersections) should be the beacon
            foreach (Pos key in candidates.Keys.OrderByDescending(k => candidates[k]))
            {
                if (CheckPos(key))
                {
                    Console.Write(TuningFrequency(key));
                }
            }
        }

        public bool CheckPos(Pos pos)
        {
            if (pos.x < 0 || pos.y < 0) return false;
            if (pos.x > 4_000_000 || pos.y > 4_000_000) return false;

            foreach (Sensor sensor in this.sensors!)
            {
                long dist = Distance(pos, sensor.pos);
                if (dist <= sensor.distanceToBeacon)
                {
                    return false;
                }
            }
            return true;
        }

        public static long TuningFrequency(Pos pos)
        {
            return 4_000_000 * pos.x + pos.y;
        }

        public static List<Pos> GetIntersectionPoints(Diamond p1, Diamond p2)
        {
            List<Pos> result = new List<Pos>();

            void Check(Pos A, Pos B, Pos C, Pos D)
            {
                (long, long)? intersection = GetIntersection(A, B, C, D);
                if (intersection == null) return;

                Pos Z = new Pos(intersection.Value.Item1, intersection.Value.Item2);

                if (OnSegment(A, B, Z) && OnSegment(C, D, Z))
                {
                    result.Add(Z);
                }
            }

            Pos A, B, C, D;
            for (int i = 1; i < p1.edges.Count; i++)
            {
                A = p1.edges[i - 1];
                B = p1.edges[i];

                for (int j = 1; j < p2.edges.Count; j++)
                {
                    C = p2.edges[j-1];
                    D = p2.edges[j];
                    
                    Check(A, B, C, D);
                }

                C = p2.edges[^1];
                D = p2.edges[0];
                Check(A, B, C, D);
            }

            A = p1.edges[^1];
            B = p1.edges[0];

            for (int j = 1; j < p2.edges.Count; j++)
            {
                C = p2.edges[j - 1];
                D = p2.edges[j];

                Check(A, B, C, D);
            }

            C = p2.edges[^1];
            D = p2.edges[0];
            Check(A, B, C, D);

            return result;
        }

        // https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection
        // Return intersection of line AB and CD
        private static (long, long)? GetIntersection(Pos a, Pos b, Pos c, Pos d)
        {
            long denominator = ((a.x - b.x) * (c.y - d.y) - (a.y - b.y) * (c.x - d.x));
            if (denominator == 0) return null;

            long x = ((a.x*b.y - a.y*b.x)*(c.x - d.x) - (a.x - b.x) * (c.x * d.y - c.y * d.x)) / denominator;
            long y = ((a.x * b.y - a.y * b.x) * (c.y - d.y) - (a.y - b.y) * (c.x * d.y - c.y * d.x)) / denominator;

            return (x, y);
        }

        private static long Distance(Pos a, Pos b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        // check if pos c on segment ab
        private static bool OnSegment(Pos a, Pos b, Pos c)
        {
            return Distance(a, c) <= Distance(a, b) && Distance(b, c) <= Distance(a, b);
        }
    }

    public struct Pos
    {
        public long x;
        public long y;
        public Pos(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Sensor
    {
        public long x;
        public long y;
        public long distanceToBeacon;
        public Pos pos;

        public Sensor(long x, long y, long distanceToBeacon)
        {
            this.x = x;
            this.y = y;
            this.distanceToBeacon = distanceToBeacon;
            this.pos = new Pos(x, y);
        }
    }

    public class Diamond
    {
        public List<Pos> edges;

        public Diamond(List<Pos> toAdd)
        {
            this.edges = toAdd;
        }
    }
}