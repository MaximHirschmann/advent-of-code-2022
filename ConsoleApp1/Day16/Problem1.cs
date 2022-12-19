using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Problem1
    {
        public static void Solve()
        {
            int totalMinutes = 30;

            string input = Solution.ReadInput();

            string pattern = @"Valve (..) has flow rate=([0-9]*); tunnel[s]? lead[s]? to valve[s]? (.*)";

            Graph G = new Graph();

            foreach (Match m in Regex.Matches(input, pattern))
            {
                string valve = m.Groups[1].Value;
                int flowRate = Convert.ToInt32(m.Groups[2].Value);
                string[] leadsTo = m.Groups[3].Value.Replace(",", "").Replace("\r", "").Split(' ');

                G.Add(valve, flowRate, leadsTo);
            }

            Dictionary<(string, string), List<(int, int)>> bestScore = new();

            int res = 0;
            HashSet<string> valvesOpen = new();
            Stack<(string, int, int, HashSet<string>, string)> stack = new();
            AddToStack("AA", 0, 0, valvesOpen, "");

            void AddToStack(string currentNode, int minutes, int score, HashSet<string> openValves, string last)
            {
                string listHash = string.Join("", openValves.Order());

                if (bestScore.ContainsKey((currentNode, listHash)))
                {
                    if (bestScore[(currentNode, listHash)].Any(item => score <= item.Item1 && minutes >= item.Item2))
                    {
                        return;
                    }
                    else
                    {
                        bestScore[(currentNode, listHash)].RemoveAll(item => item.Item1 <= score && item.Item2 >= minutes);
                        bestScore[(currentNode, listHash)].Add((score, minutes));
                    }
                }
                else
                {
                    bestScore[(currentNode, listHash)] = new List<(int, int)>() { (score, minutes) };
                }

                stack.Push((
                    currentNode,
                    minutes,
                    score,
                    openValves,
                    last
                    ));
            }

            while (stack.Count > 0)
            {
                (string currentNode, int minutes, int score, HashSet<string> openValves, string last) = stack.Pop();

                if (minutes == totalMinutes)
                {
                    res = int.Max(res, score);
                    continue;
                }

                if (!openValves.Contains(currentNode) && !(G.flowRates[currentNode] == 0))
                {
                    AddToStack(
                        currentNode,
                        minutes + 1,
                        score + (totalMinutes - minutes - 1) * G.flowRates[currentNode],
                        new HashSet<string>(openValves) { currentNode },
                        currentNode
                        );
                }

                foreach (string to in G.goesTo[currentNode])
                {
                    if (to == last) continue;

                    AddToStack(
                           to,
                           minutes + 1,
                           score,
                           openValves,
                           currentNode
                        );
                }
            }

            Console.WriteLine(res);
            return;
        }
    }

    class Graph
    {
        public Dictionary<string, HashSet<string>> goesTo;
        public Dictionary<string, int> flowRates;

        public Graph()
        {
            this.goesTo = new();
            this.flowRates = new();
        }

        public void Add(string key, int flowRate, string[] leadsTo)
        {
            this.flowRates[key] = flowRate;

            foreach (string to in leadsTo)
            {
                AddEdge(key, to);
            }
        }

        private void AddEdge(string from, string to)
        {
            if (!this.goesTo.ContainsKey(from))
            {
                this.goesTo[from] = new();
            }

            this.goesTo[from].Add(to);
        }
    }
}