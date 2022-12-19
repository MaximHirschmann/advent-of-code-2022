using System.Collections.Generic;
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

                G.Add(valve, flowRate, leadsTo);
            }

            Dictionary<(string, string, string), List<(int, int)>> bestScore = new();

            int res = 0;
            HashSet<string> valvesOpen = new();
            Stack<(string, string, int, int, HashSet<string>, string, string)> stack = new();
            AddToStack("AA", "AA", 0, 0, valvesOpen, "", "");

            void AddToStack(string currentNode, string elephantNode, int minutes, int score, HashSet<string> openValves, string last, string lastElephant)
            {
                string listHash = string.Join("", openValves.Order());

                if (bestScore.ContainsKey((currentNode, elephantNode, listHash)))
                {
                    if (bestScore[(currentNode, elephantNode, listHash)].Any(item => score <= item.Item1 && minutes >= item.Item2))
                    {
                        return;
                    }
                    else
                    {
                        bestScore[(currentNode, elephantNode, listHash)].RemoveAll(item => item.Item1 <= score && item.Item2 >= minutes);
                        bestScore[(currentNode, elephantNode, listHash)].Add((score, minutes));
                    }
                }
                else
                {
                    bestScore[(currentNode, elephantNode, listHash)] = new List<(int, int)>() { (score, minutes) };
                }

                stack.Push((
                    currentNode,
                    elephantNode,
                    minutes,
                    score,
                    openValves,
                    last,
                    lastElephant
                    ));
            }

            while (stack.Count > 0)
            {
                (string currentNode, string elephantNode, int minutes, int score, HashSet<string> openValves, string last, string lastElephant) = stack.Pop();

                if (minutes == totalMinutes)
                {
                    res = int.Max(res, score);
                    continue;
                }

                // new node, add to score, opened valve, new last
                Stack <(string, int, string, string)> newPerson = new();
                Stack <(string, int, string, string)> newElephant = new();

                if (!openValves.Contains(currentNode) && !(G.flowRates[currentNode] == 0))
                {
                    newPerson.Push((
                        currentNode,
                        (totalMinutes - minutes - 1) * G.flowRates[currentNode],
                        currentNode,
                        currentNode
                        ));
                }
                if (!openValves.Contains(elephantNode) && !(G.flowRates[elephantNode] == 0))
                {
                    newElephant.Push((
                        elephantNode,
                        (totalMinutes - minutes - 1) * G.flowRates[elephantNode],
                        elephantNode,
                        elephantNode
                        ));
                }

                foreach (string to in G.goesTo[currentNode])
                {
                    if (to == last) continue;

                    newPerson.Push((
                        to,
                        0,
                        "",
                        currentNode
                        ));
                }
                foreach (string to in G.goesTo[elephantNode])
                {
                    if (to == lastElephant) continue;

                    newElephant.Push((
                        to,
                        0,
                        "",
                        elephantNode
                        ));
                }

                for (int i = 0; i < newPerson.Count; i++)
                {
                    for (int j = 0; j < newElephant.Count; j++) {
                        (string newPersonNode, int personScore, string personValve, string lastPerson) = newPerson.ElementAt(i);
                        (string newElephantNode, int elephantScore, string elephantValve, string lastElephantNode) = newElephant.ElementAt(j);

                        if (personScore > 0 && elephantScore > 0 && personValve == elephantValve) continue;

                        HashSet<string> newOpenValves = new(openValves);
                        if (personValve != "") newOpenValves.Add(personValve);
                        if (elephantValve != "") newOpenValves.Add(elephantValve);

                        AddToStack(
                            newPersonNode,
                            newElephantNode,
                            minutes + 1,
                            score + personScore + elephantScore,
                            newOpenValves,
                            lastPerson,
                            lastElephantNode
                            );
                    }
                }
            }

            Console.WriteLine(res);
            return;
        }
    }
}