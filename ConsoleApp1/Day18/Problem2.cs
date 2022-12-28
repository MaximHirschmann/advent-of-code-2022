namespace Day18
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            List<(int, int, int)> neighbors = new()
            {
                (0, 0, 1),
                (0, 0, -1),
                (0, 1, 0),
                (0, -1, 0),
                (1, 0, 0),
                (-1, 0, 0)
            };
            
            HashSet<(int, int, int)> points = new();

            int maxX = 0;
            int maxY = 0;
            int maxZ = 0;

            int result = 0;
            foreach (string line in input)
            {
                string[] parts = line.Split(",");

                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                int z = int.Parse(parts[2]);
                
                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);
                maxZ = Math.Max(maxZ, z);
                
                points.Add((x, y, z));

                result += 6;

                foreach ((int dx, int dy, int dz) in neighbors)
                {
                    if (points.Contains((x + dx, y + dy, z + dz)))
                    {
                        result -= 2;
                    }
                }
            }
            
            // union find
            Dictionary<(int, int, int), (int, int, int)> roots = new();
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    for (int z = 0; z <= maxZ; z++)
                    {
                        roots[(x, y, z)] = (x, y, z);
                    }
                }
            } 
            
            (int, int, int) FindRoot((int, int, int) point)
            {
                if (roots[point] == point)
                {
                    return point;
                }

                return roots[point] = FindRoot(roots[point]);
            }

            void Union((int, int, int) point1, (int, int, int) point2)
            {
                (int, int, int) rootI = FindRoot(point1);
                (int, int, int) rootJ = FindRoot(point2);

                if (rootI != rootJ)
                {
                    roots[rootI] = rootJ;
                }
            }

            bool IsOutOfBounds(int x, int y, int z)
            {
                return x < 0 || y < 0 || z < 0 || x > maxX || y > maxY || z > maxZ;
            }

            bool IsOnBorder(int x, int y, int z)
            {
                return x == 0 || y == 0 || z == 0 || x == maxX || y == maxY || z == maxZ;
            }

            foreach ((int, int, int) point in roots.Keys)
            {
                (int x, int y, int z) = roots[point];
                bool isLava = points.Contains((x, y, z));

                foreach ((int dx, int dy, int dz) in neighbors)
                {
                    (int x2, int y2, int z2) = (x + dx, y + dy, z + dz);
                    bool isLava2 = points.Contains((x2, y2, z2));

                    if (!IsOutOfBounds(x2, y2, z2) && isLava == isLava2)
                    {
                        Union((x, y, z), (x2, y2, z2));
                    }
                }
            }

            HashSet<(int, int, int)> airRootSet = new();
            foreach ((int, int, int) point in roots.Keys)
            {
                (int, int, int) root = FindRoot(point);
                (int x, int y, int z) = root;
                
                if (
                    root == point && 
                    !points.Contains(root) && 
                    !IsOutOfBounds(x, y, z) && 
                    !IsOnBorder(x, y, z))
                {
                    airRootSet.Add(point);
                }
            }
            
            foreach ((int, int, int) point in airRootSet)
            {
                HashSet<(int, int, int)> cluster = new() {};
                int clusterSurface = 0;
                
                foreach ((int, int, int) point2 in roots.Keys)
                {
                    if (FindRoot(point2) == point)
                    {
                        clusterSurface += 6;
                        cluster.Add(point2);
                    }
                }

                foreach ((int x, int y, int z) in cluster)
                {
                    foreach ((int dx, int dy, int dz) in neighbors)
                    {
                        var neighbor = (x + dx, y + dy, z + dz);
                        if (cluster.Contains(neighbor))
                        {
                            clusterSurface = Math.Max(0, clusterSurface - 1);
                        }
                        else if (!points.Contains(neighbor))
                        {
                            clusterSurface = 0;
                        }
                    }
                }

                result -= clusterSurface;
            }

            Console.WriteLine(result);
            
            return;
        }
        
    }
}