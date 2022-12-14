namespace Day7
{
    class Problem2
    {
        public static void Solve()
        {
            string[] input = Solution.ReadInput();

            Directory top = new Directory();
            Directory d = top;
            bool ls = false;

            foreach (string line in input)
            {
                if (line.Contains("ls"))
                {
                    ls = true;
                    continue;
                }

                if (line.Contains("$"))
                {
                    ls = false;
                }

                if (line == "$ cd /") continue;
                else if (line == "$ cd ..")
                {
                    d = (Directory)d.parent!;
                }
                else if (line.Contains("$ cd"))
                {
                    string dirName = line.Split(" ")[2];
                    d = d.NavigateTo(dirName);
                }
                if (ls)
                {
                    if (line.Contains("dir "))
                    {
                        string dirName = line.Split(" ")[1];
                        d.CreateDir(dirName);
                    }
                    else
                    {
                        string[] split = line.Split(" ");
                        int fileSize = int.Parse(split[0]);
                        string fileName = split[1];

                        d.CreateFile(fileSize, fileName);
                    }
                }
            }

            int needed = 30000000 - (70000000 - top.GetSize());
            // assume needed is positve

            // dfs
            Directory res = top;
            Stack<Directory> stack = new();
            stack.Push(top);

            while (stack.Count > 0)
            {
                Directory elem = stack.Pop();
                if (elem.GetSize() >= needed && elem.GetSize() < 2224309) 
                {
                    Console.WriteLine(elem.ToString());
                }
                if (elem.GetSize() >= needed && elem.GetSize() < res.GetSize())
                {
                    res = elem;
                }
                foreach (Directory temp in elem.children.Where(c => c is Directory))
                {
                    stack.Push(temp);
                }
            }

            Console.WriteLine(res.GetSize());

            return;
        }
    }
}