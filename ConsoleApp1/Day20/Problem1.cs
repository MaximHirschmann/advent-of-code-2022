namespace Day20;
class Problem1
{
    public static void Solve()
    {
        string [] input = Solution.ReadInput();

        List<(int, int)> ints = input.Select((line, index) => (int.Parse(line), index)).ToList();

        // mod function that works with negative numbers
        // -1 % 6 -> 5
        int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        int n = ints.Count;

        for (int i = 0; i < n; i++)
        {
            // find the position of the previously i'th element
            int index = ints.FindIndex(item => item.Item2 == i);
            
            int value = ints[index].Item1;
            int new_index = mod(index + value, n-1);

            ints.RemoveAt(index);
            ints.Insert(new_index, (value, i));
        }

        int zero_index = ints.FindIndex(m => m.Item1 == 0);

        int ValueAt(int zero_index, int shift)
        {
            return ints[(zero_index + shift) % n].Item1;
        }

        int res = ValueAt(zero_index, 1000) + ValueAt(zero_index, 2000) + ValueAt(zero_index, 3000);

        Console.WriteLine(res);
    }
}