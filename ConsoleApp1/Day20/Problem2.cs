namespace Day20;
class Problem2
{
    public static void Solve()
    {
        string[] input = Solution.ReadInput();

        List<(long, int)> ints = input.Select((line, index) => (long.Parse(line) * 811589153, index)).ToList();
        
        // mod function that works with negative numbers
        // -1 % 6 -> 5
        long mod(long x, int m)
        {
            return (x % m + m) % m;
        }

        int n = ints.Count;

        for (int loop = 0; loop < 10; loop++)
        {
            for (int i = 0; i < n; i++)
            {
                // find the position of the previously i'th element
                int index = ints.FindIndex(item => item.Item2 == i);
                
                long value = ints[index].Item1;
                int new_index = (int) mod(index + value, n - 1);

                ints.RemoveAt(index);
                ints.Insert(new_index, (value, i));
            }
        }

        int zero_index = ints.FindIndex(m => m.Item1 == 0);
        
        long ValueAt(int zero_index, int shift)
        {
            return ints[(zero_index + shift) % n].Item1;
        }

        long res = ValueAt(zero_index, 1000) + ValueAt(zero_index, 2000) + ValueAt(zero_index, 3000);

        Console.WriteLine(res);
    }
}