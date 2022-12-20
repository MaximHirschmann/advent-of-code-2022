using System.Diagnostics;
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

                G.AddNode(valve, flowRate, leadsTo);
            }

            WeightedGraph WG = new WeightedGraph(G);

            Dictionary<(string, long), List<(int, int)>> bestScore = new();

            Dictionary<string, int> valveToPrime = new();
            int[] primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251 };
            for (int i = 0; i < WG.flowRates.Keys.Count; i++)
            {
                valveToPrime[WG.flowRates.Keys.ElementAt(i)] = primes[i];
            }

            int res = 0;
            List<(string, int)> resHistory = new();
            Stack<(string, int, int, long, string, List<(string, int)>)> stack = new();
            AddToStack("AA", 0, 0, 1, "", new());

            void AddToStack(string currentNode, int minutes, int score, long openValves, string last, List<(string, int)> history)
            {
                if (bestScore.ContainsKey((currentNode, openValves)))
                {
                    if (bestScore[(currentNode, openValves)].Any(item => score <= item.Item1 && minutes >= item.Item2))
                    {
                        return;
                    }
                    else
                    {
                        bestScore[(currentNode, openValves)].RemoveAll(item => item.Item1 <= score && item.Item2 >= minutes);
                        bestScore[(currentNode, openValves)].Add((score, minutes));
                    }

                }
                else
                {
                    bestScore[(currentNode, openValves)] = new List<(int, int)>() { (score, minutes) };
                }

                stack.Push((
                    currentNode,
                    minutes,
                    score,
                    openValves,
                    last,
                    history
                    ));
            }

            while (stack.Count > 0)
            {
                (string currentNode, int minutes, int score, long openValves, string last, List<(string, int)> history) = stack.Pop();

                if (score > res && minutes <= totalMinutes)
                {
                    res = score;
                    resHistory = history;
                }
                if (minutes >= totalMinutes)
                {
                    continue;
                }

                if (openValves % valveToPrime[currentNode] != 0 && WG.flowRates[currentNode] != 0)
                {
                    AddToStack(
                        currentNode,
                        minutes + 1,
                        score + (totalMinutes - minutes - 1) * WG.flowRates[currentNode],
                        openValves * valveToPrime[currentNode],
                        currentNode,
                        new List<(string, int)>(history) { (currentNode, minutes)}
                        );
                }

                
                foreach ((string to, int weight) in WG.goesTo[currentNode])
                {
                    if (to == last) continue;

                    AddToStack(
                           to,
                           minutes + weight,
                           score,
                           openValves,
                           currentNode,
                           history
                        );
                }
            }

            Console.WriteLine(res);
            foreach ((string s, int i) in resHistory)
            {
                Console.Write($"({s}, {i}), ");
            }

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

        public void AddNode(string key, int flowRate, string[] leadsTo)
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

    class WeightedGraph
    {
        public Dictionary<string, HashSet<(string, int)>> goesTo;
        public Dictionary<string, int> flowRates;

        public WeightedGraph(Graph G)
        {
            this.flowRates = G.flowRates;

            Dictionary<string, HashSet<(string, int)>> newGoesTo = new();
            foreach (string key in G.goesTo.Keys)
            {
                HashSet<(string, int)> newHS = new();
                foreach (string to in G.goesTo[key])
                {
                    newHS.Add((to, 1));
                }

                newGoesTo[key] = newHS;
            }

            while (true)
            {
                string toRemove = "";
                foreach (string key in this.flowRates.Keys)
                {
                    if (this.flowRates[key] == 0 && key != "AA")
                    {
                        toRemove = key;
                        break;
                    }
                }
                if (toRemove == "") break;

                foreach ((string neighbor, int weight) in newGoesTo[toRemove])
                {
                    newGoesTo[neighbor].Remove((toRemove, weight));
                    HashSet<(string, int)> toAdd = new();
                    foreach ((string neighbor2, int weight2) in newGoesTo[toRemove]) {
                        if (neighbor2 == neighbor) continue;

                        if (!newGoesTo[neighbor].Any(item => item.Item1 == neighbor2 && item.Item2 < weight + weight2))
                        {
                            toAdd.Add((neighbor2, weight + weight2));
                        }

                    }
                    newGoesTo[neighbor].UnionWith(toAdd);
                }

                newGoesTo.Remove(toRemove);
                this.flowRates.Remove(toRemove);
            }
            this.goesTo = newGoesTo;

            return;
        }
    }
}