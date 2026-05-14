using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine().Trim());
        
        int minA = int.MaxValue;
        int minB = int.MaxValue;
        int minC = int.MaxValue;
        
        int[][] allDims = new int[n][];
        for (int i = 0; i < n; i++)
        {
            var parts = Console.ReadLine().Trim().Split(' ');
            allDims[i] = new int[] {
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])
            };
        }
        
        Stopwatch stopwatch = Stopwatch.StartNew();
        long memoryBefore = GC.GetTotalMemory(true);
        
        for (int i = 0; i < n; i++)
        {
            int[] dims = allDims[i];
            Array.Sort(dims);
            
            minA = Math.Min(minA, dims[0]);
            minB = Math.Min(minB, dims[1]);
            minC = Math.Min(minC, dims[2]);
        }
        
        long volume = (long)minA * minB * minC;
        
        long memoryAfter = GC.GetTotalMemory(true);
        stopwatch.Stop();
        
        Console.WriteLine($"Результат: {volume}");
        Console.WriteLine($"Время работы АЛГОРИТМА: {stopwatch.Elapsed.TotalSeconds:F6} сек");
        Console.WriteLine($"Использовано памяти: {(memoryAfter - memoryBefore) / 1024.0:F2} КБ");
    }
}