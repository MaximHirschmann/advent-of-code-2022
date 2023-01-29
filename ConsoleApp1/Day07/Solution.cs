namespace Day7
{
    public class Solution
    {
        public static void Solve()
        {
            Problem1.Solve();
            Problem2.Solve();
        }

        public static string[] ReadInput()
        {
            string thisFIlePath = new System.Diagnostics.StackTrace(true).GetFrame(0)!.GetFileName()!;
            string directoryPath = System.IO.Path.GetDirectoryName(thisFIlePath)!;
            string inputTxtPath = directoryPath + @"\input.txt";
            
            string[] lines = System.IO.File.ReadAllLines(inputTxtPath);

            return lines;
        }
    }

    public class Directory : File
    {
        public List<File> children;

        public Directory() {
            this.children = new List<File>();
        }

        public Directory(Directory parent, string name) {
            this.parent = parent;
            this.children = new List<File>();
            this.name = name;
        }

        public Directory NavigateTo(string name)
        {
            return (Directory) this.children.Where(c => c.name == name).First();
        }


        public void CreateDir(string name)
        {
            this.size = null;
            this.children.Add(new Directory(this, name));
        }

        public void CreateFile(int fileSize, string fileName)
        {
            if (this.size != null)
            {
                this.size += fileSize;
            }
            this.children.Add(new File(fileName, fileSize, this));
        }

        public override int GetSize()
        {
            if (this.size != null)
            {
                return (int) this.size;
            }
            int res = 0;
            foreach (File f in this.children)
            {
                res += f.GetSize();
            }
            this.size = res;
            return res;
        }

        public override string ToString()
        {
            if (this.name == "")
            {
                return "root";
            }

            if (this.size == null)
            {
                return "Dir " + this.name;
            }
            else
            {
                return "Dir" + this.name + " Size: " + this.size.ToString();
            }
        }
    }

    public class File
    {
        protected int? size;
        public File? parent;
        public string name;

        public File()
        {
            this.size = null;
            this.parent = null;
            this.name = "";
        }
        public File(string name, int size, Directory parent)
        {
            this.name = name;
            this.size = size;
            this.parent = parent;
        }

        public virtual int GetSize()
        {
            if (this.size == null)
            {
                return 0;
            }
            return (int) this.size;
        }

        public override string ToString()
        {
            return "File " + this.name;
        }
    }
}