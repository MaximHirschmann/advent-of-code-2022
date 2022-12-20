using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day16
{
    class Problem2
    {
        public static void Solve()
        {
            int totalMinutes = 26;

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

            Dictionary<(string, string, long), List<(int, int)>> bestScore = new();
            Dictionary<string, int> valveToPrime = new();
            int[] primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251 };
            for (int i = 0; i < WG.flowRates.Keys.Count; i++)
            {
                valveToPrime[WG.flowRates.Keys.ElementAt(i)] = primes[i];
            }

            int res = 0;
            List<(string, string, int)> resHistory = new();
            long valvesOpen = 1;
            Stack<(string, string, int, int, int, long, string, string, List<(string, string, int)>)> stack = new();
            AddToStack("AA", "AA", 0, 0, 0, valvesOpen, "", "", new());

            void AddToStack(string currentNode, string elephantNode, int minutes, int elephantMinutes, int score, long openValves, string last, string lastElephant, List<(string, string, int)> history)
            {
                if (bestScore.ContainsKey((currentNode, elephantNode, openValves)))
                {
                    if (bestScore[(currentNode, elephantNode, openValves)].Any(item => score <= item.Item1 && minutes >= item.Item2))
                    {
                        return;
                    }
                    else
                    {
                        bestScore[(currentNode, elephantNode, openValves)].RemoveAll(item => item.Item1 <= score && item.Item2 >= minutes);
                        bestScore[(currentNode, elephantNode, openValves)].Add((score, minutes));
                    }
                }
                else
                {
                    bestScore[(currentNode, elephantNode, openValves)] = new List<(int, int)>() { (score, minutes) };
                }

                stack.Push((
                    currentNode,
                    elephantNode,
                    minutes,
                    elephantMinutes,
                    score,
                    openValves,
                    last,
                    lastElephant,
                    history
                    ));
            }

            Stopwatch sw = Stopwatch.StartNew();
            while (stack.Count > 0)
            {
                (string currentNode, string elephantNode, int minutes, int elephantMinutes, int score, long openValves, string last, string lastElephant, List<(string, string, int)> history) = stack.Pop();

                if (score > res && minutes <= totalMinutes && elephantMinutes <= totalMinutes)
                {
                    Console.WriteLine($"{score} - {sw.Elapsed}");

                    res = score;
                    resHistory = history;
                }
                if (minutes > totalMinutes || elephantMinutes > totalMinutes)
                {
                    continue;
                }

                // new node, add to score, opened valve, new last, add to time
                Stack<(string, int, string, string, int)> newPerson = new();
                Stack <(string, int, string, string, int)> newElephant = new();

                if (openValves % valveToPrime[currentNode] != 0 && WG.flowRates[currentNode] != 0)
                {
                    newPerson.Push((
                        currentNode,
                        (totalMinutes - minutes - 1) * WG.flowRates[currentNode],
                        currentNode,
                        currentNode,
                        1
                        ));
                }
                if (openValves % valveToPrime[elephantNode] != 0 && !(WG.flowRates[elephantNode] == 0))
                {
                    newElephant.Push((
                        elephantNode,
                        (totalMinutes - elephantMinutes - 1) * WG.flowRates[elephantNode],
                        elephantNode,
                        elephantNode,
                        1
                        ));
                }

                foreach ((string to, int weight) in WG.goesTo[currentNode])
                {
                    if (to == last) continue;

                    newPerson.Push((
                        to,
                        0,
                        "",
                        currentNode,
                        weight
                        ));
                }
                foreach ((string to, int weight) in WG.goesTo[elephantNode])
                {
                    if (to == lastElephant) continue;

                    newElephant.Push((
                        to,
                        0,
                        "",
                        elephantNode,
                        weight
                        ));
                }


                if (newPerson.Count == 0)
                {
                    newPerson.Push((currentNode, 0, "", last, 0));
                }
                if (newElephant.Count == 0)
                {
                    newElephant.Push((elephantNode, 0, "", lastElephant, 0));
                }
                for (int i = 0; i < newPerson.Count; i++)
                {
                    for (int j = 0; j < newElephant.Count; j++) {
                        (string newPersonNode, int personScore, string personValve, string lastPerson, int personTime) = newPerson.ElementAt(i);
                        (string newElephantNode, int elephantScore, string elephantValve, string lastElephantNode, int elephantTime) = newElephant.ElementAt(j);

                        if (personScore > 0 && elephantScore > 0 && personValve == elephantValve) continue;
                        if (personTime == 0 && elephantTime == 0) continue;

                        long newOpenValves = openValves;
                        List<(string, string, int)> newHistory = new(history);
                        if (personValve != "")
                        {
                            newOpenValves *= valveToPrime[personValve];
                            newHistory.Add(("P", personValve, minutes));
                        }
                        if (elephantValve != "")
                        {
                            newOpenValves *= valveToPrime[elephantValve];
                            newHistory.Add(("E", elephantValve, elephantMinutes));
                        }
                        AddToStack(
                            newPersonNode,
                            newElephantNode,
                            minutes + personTime,
                            elephantMinutes + elephantTime,
                            score + personScore + elephantScore,
                            newOpenValves,
                            lastPerson,
                            lastElephantNode,
                            newHistory
                            );
                    }
                }
            }

            Console.WriteLine(res);
            foreach ((string s, string s2, int i) in resHistory)
            {
                Console.Write($"({s}, {s2}, {i}), ");
            }
            return;
        }
    }
}