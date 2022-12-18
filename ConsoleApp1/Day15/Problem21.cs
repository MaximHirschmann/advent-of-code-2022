using System.Text.RegularExpressions;

namespace Day15
{
    class Problem21
    {
        public static void Solve()
        {
            string input = Solution.ReadInput();

            string pattern = @"Sensor at x=(-?[0-9]*), y=(-?[0-9]*): closest beacon is at x=(-?[0-9]*), y=(-?[0-9]*)";

            List<Equation> equations = new List<Equation>();

            foreach (Match m in Regex.Matches(input, pattern))
            {
                int sensor_x = Convert.ToInt32(m.Groups[1].ToString());
                int sensor_y = Convert.ToInt32(m.Groups[2].ToString());
                int beacon_x = Convert.ToInt32(m.Groups[3].ToString());
                int beacon_y = Convert.ToInt32(m.Groups[4].ToString());

                int distanceToBeacon = Math.Abs(sensor_x - beacon_x) + Math.Abs(sensor_y - beacon_y);

                Equation equ = new Equation(sensor_x, sensor_y, distanceToBeacon);
                equations.Add(equ);
            }

            var byX = equations.OrderBy(equ => equ.x).ToList();
            var byY = equations.OrderBy(equ => equ.y).ToList();

            for (int i = 1; i < byX.Count; i++)
            {
                int XLimit = byX[i-1].x;

                for (int j = 1; j < byY.Count; j++)
                {
                    int YLimit = byY[j-1].y;

                    Console.WriteLine(XLimit);
                    Console.WriteLine(YLimit);
                    int max1 = int.MinValue;
                    int max2 = int.MinValue;
                    int max3 = int.MinValue;
                    int max4 = int.MinValue;

                    foreach (Equation equation in equations)
                    {
                        Console.WriteLine(equation.ToStringCondition(XLimit, YLimit));

                        int newC = equation.c;

                        switch (equation.x <= XLimit, equation.y <= YLimit)
                        {
                            case (true, true):
                                newC += equation.x + equation.y;
                                max1 = int.Max(max1, newC);
                                break;
                            case (true, false):
                                newC += equation.x - equation.y;
                                max2 = int.Max(max2, newC);
                                break;
                            case (false, true):
                                newC += -equation.x + equation.y;
                                max3 = int.Max(max3, newC);
                                break;
                            case (false, false):
                                newC += -equation.x - equation.y;
                                max4 = int.Max(max4, newC);
                                break;
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("x + y > " + max1.ToString());
                    Console.WriteLine("x - y > " + max2.ToString());
                    Console.WriteLine("-x + y > " + max3.ToString());
                    Console.WriteLine("-x - y > " + max4.ToString());
                    Console.WriteLine();

                }
            }
        }
    }

    class Equation
    {
        public int x;
        public int y;
        public int c;

        public Equation(int x, int y, int c) 
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }

        public override string ToString()
        {
            string res = "|x - " + this.x.ToString() + "| + |y - " + this.y.ToString() + "| - " + this.c.ToString() + " > 0";

            return res;
        }


        public int NewC(int XLim, int YLim)
        {
            int newC = -this.c;
            newC += this.x <= XLim ? -this.x : this.x;
            newC += this.y <= YLim ? -this.y : this.y;

            return -newC;
        }

        public string ToStringCondition(int XLim, int YLim)
        {

            string res = "";
            res += this.x <= XLim ? "x" : "-x";
            res += this.y <= YLim ? " + y" : " - y";

            int newC = this.NewC(XLim, YLim);

            res += " > " + newC.ToString();

            return res;
        }
    }
}