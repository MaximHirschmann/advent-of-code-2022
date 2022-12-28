namespace Day17
{
    class Problem1
    {
        public static void Solve()
        {
            string input = Solution.ReadInput();

            Chamber chamber = new Chamber(input);

            for (int i = 0; i < 2022; i++)
            {
                chamber.DropBlock(i % 5);
            }

            Console.WriteLine(chamber.currentHighest);
            return;
        }
    }

    class Chamber
    {
        public List<Block> stoppedBlocks;
        public int currentHighest;
        string jets;
        int jetsIndex;
        Block? falling;
        Dictionary<(int, string, int), (int, int)> memory;
        int[] currentHighestArray;
        int counter = 0;

        public Chamber(string jets, int jetsIndex = 0, string bottom = "")
        {
            this.stoppedBlocks = new();
            this.currentHighest = 0;
            this.jets = jets;
            this.jetsIndex = jetsIndex;
            this.falling = null;
            this.memory = new();
            this.currentHighestArray = new int[7];

            if (bottom != "")
            {
                int[] bottomInt = bottom
                    .Remove(bottom.Length - 1)
                    .Split(",")
                    .Select(c => Convert.ToInt32(c))
                    .ToArray();

                this.currentHighest = bottomInt.Max() + 1;
                List<Pos> positions = new();
                for (int i = 0; i < 7; i++)
                {
                    positions.Add(new Pos(i, this.currentHighest - bottomInt[i] - 1));
                }
                Block block = new Block(positions);

                this.stoppedBlocks.Add(block);
            }
        }

        private char GetNextJet()
        {
            char res = this.jets[this.jetsIndex];
            this.jetsIndex = (this.jetsIndex + 1) % this.jets.Length;
            return res;
        }

        private void SolidifyBlock()
        {
            if (this.falling == null)
                throw new Exception("No block to solidify");

            this.stoppedBlocks.Add(this.falling);

            foreach (Pos block in this.falling.blocks)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (block.x + this.falling.bottomLeft.x == i)
                    {
                        this.currentHighestArray[i] = int.Max(this.currentHighestArray[i], block.y + this.falling.bottomLeft.y);
                    }
                }
            }
            this.currentHighest = int.Max(this.currentHighest, this.falling.bottomLeft.y + this.falling.height - 1);
            this.counter++;
        }

        private string GetStringRepresentationOfHighest()
        {
            string res = "";
            foreach (int i in this.currentHighestArray)
            {
                res += (this.currentHighest - i).ToString() + ",";
            }
            return res;
        }

        public (int, int, int, int, int, string, int) GetRepeating()
        {
            int type = 0;
            while (true)
            {
                string s = GetStringRepresentationOfHighest();

                if (this.memory.ContainsKey((type, s, this.jetsIndex)))
                {
                    (int old_index, int old_height) = this.memory[(type, s, this.jetsIndex)];
                    int idxDiff = this.counter - old_index;
                    int heightDiff = this.currentHighest - old_height;
                    return (heightDiff, idxDiff, old_height, old_index, this.jetsIndex, s, type);
                }

                this.memory[(type, s, this.jetsIndex)] = (this.counter, this.currentHighest);

                DropBlock(type);
                type = (type + 1) % 5;
            }
        }

        public void DropBlock(int type)
        {
            Pos bottomLeft = new Pos(2, this.currentHighest + 4);
            this.falling = new Block(type, bottomLeft);
            while (true)
            {
                char jet = GetNextJet();
                this.BlowBlock(jet);

                if (!this.TryToDropBlock())
                {
                    break;
                }
            }
            SolidifyBlock();
        }

        private void BlowBlock(char jet)
        {
            int dx = 0;
            if (jet == '<')
                dx = -1;
            else if (jet == '>')
                dx = 1;

            this.falling!.bottomLeft.x += dx;

            if (this.falling.IsOutsideOfBounds() || CollidesWithAnyBlock(this.falling))
            {
                this.falling.bottomLeft.x -= dx;
            }
        }

        private bool TryToDropBlock()
        {
            if (this.falling!.bottomLeft.y == 1)
            {
                return false;
            }

            this.falling.bottomLeft.y -= 1;

            if (CollidesWithAnyBlock(this.falling))
            {
                this.falling.bottomLeft.y += 1;
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CollidesWithAnyBlock(Block block)
        {
            // only look at the last 50 dropped blocks
            return this.stoppedBlocks.TakeLast(50).Any(other => block.CollidesWith(other));
        }

        public void Print()
        {
            int m = this.currentHighest + 1;
            int n = 9;
            char[,] array = new char[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = '.';
                }
            }

            for (int i = 0; i < m; i++)
            {
                array[i, 0] = '|';
                array[i, 8] = '|';
            }
            for (int i = 0; i < n; i++)
            {
                array[0, i] = '-';
            }
            array[0, 0] = '+';
            array[0, 8] = '+';

            foreach (Block block in this.stoppedBlocks)
            {
                foreach (Pos pos in block.blocks)
                {
                    array[pos.y + block.bottomLeft.y, pos.x + block.bottomLeft.x + 1] = '#';
                }
            }

            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    struct Pos
    {
        public int x;
        public int y;

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Block
    {
        public List<Pos> blocks; // relative
        public int width;
        public int height;
        public Pos bottomLeft; // absolute
        int type;

        // custom Block
        public Block(List<Pos> positions)
        {
            this.blocks = positions;
            this.width = positions.Select(p => p.x).Max() - positions.Select(p => p.x).Min();
            this.height = positions.Select(p => p.y).Max() - positions.Select(p => p.y).Min();
            this.bottomLeft = new Pos(0, 1);
            this.type = 5;
        }

        public Block(int type, Pos bottomLeft)
        {
            this.type = type;
            this.bottomLeft = bottomLeft;
            if (type == 0)
            {
                this.blocks = new() {
                        new Pos(0, 0),
                        new Pos(1, 0),
                        new Pos(2, 0),
                        new Pos(3, 0)
                    };
                this.width = 4;
                this.height = 1;
            }
            else if (type == 1)
            {
                this.blocks = new()
                {
                    new Pos(1, 0),
                    new Pos(1, 1),
                    new Pos(0, 1),
                    new Pos(2, 1),
                    new Pos(1, 2)
                };
                this.width = 3;
                this.height = 3;
            }
            else if (type == 2)
            {
                this.blocks = new()
                {
                    new Pos(2, 2),
                    new Pos(2, 1),
                    new Pos(2, 0),
                    new Pos(1, 0),
                    new Pos(0, 0)
                };
                this.width = 3;
                this.height = 3;
            }
            else if (type == 3)
            {
                this.blocks = new()
                {
                    new Pos(0, 3),
                    new Pos(0, 2),
                    new Pos(0, 1),
                    new Pos(0, 0)
                };
                this.width = 1;
                this.height = 4;
            }
            else
            {
                this.blocks = new() {
                        new Pos(0, 0),
                        new Pos(1, 0),
                        new Pos(0, 1),
                        new Pos(1, 1)
                    };
                this.width = 2;
                this.height = 2;
            }
        }

        internal bool IsOutsideOfBounds()
        {
            if (this.bottomLeft.x < 0)
            {
                return true;
            }
            if (this.bottomLeft.x + this.width > 7)
            {
                return true;
            }
            return false;
        }

        internal bool CollidesWith(Block other)
        {
            if (Math.Abs(this.bottomLeft.y - other.bottomLeft.y) > 10)
            {
                return false;
            }
            foreach (Pos pos in this.blocks)
            {
                if (other.blocks.Any(otherPos => otherPos.x + other.bottomLeft.x == pos.x + this.bottomLeft.x && otherPos.y + other.bottomLeft.y == pos.y + this.bottomLeft.y))
                {
                    return true;
                }
            }
            return false;
        }
    }
}