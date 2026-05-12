using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) return;

        GC.Collect();
        long startMemory = GC.GetTotalMemory(true);
        Stopwatch sw = Stopwatch.StartNew();
        long n = long.Parse(input);
        
        if (n == 0)
        {
            Console.WriteLine("0");
        }
        else
        {
            List<int> bytes = new List<int>();
            while (n > 0)
            {
                bytes.Add((int)(n & 0x7F));
                n >>= 7;
            }
            bytes.Reverse();
            for (int i = 0; i < bytes.Count - 1; i++)
            {
                bytes[i] |= 128;
            }
            Console.WriteLine(string.Join(" ", bytes));
        }

        sw.Stop();
        long endMemory = GC.GetTotalMemory(false);
        Console.Error.WriteLine($"Time: {sw.Elapsed.TotalSeconds}s");
        Console.Error.WriteLine($"Memory: {(endMemory - startMemory) / 1024.0 / 1024.0}MB");
    }
}