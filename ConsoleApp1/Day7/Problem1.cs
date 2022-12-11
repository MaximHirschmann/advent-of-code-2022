namespace Day7
{
    class Problem1
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
                    d = (Directory) d.parent!;
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

            // dfs
            int res = 0;
            Stack<Directory> stack = new();
            stack.Push(top);

            while (stack.Count > 0)
            {
                Directory elem = stack.Pop();
                if (elem.GetSize() < 100000)
                {
                    res += elem.GetSize();
                }
                foreach (Directory temp in elem.children.Where(c => c is Directory))
                {
                    stack.Push(temp);
                }
            }

            Console.WriteLine(res);

            return;
        }
    }
}