using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Linq;

class Program
{
    static void Main()
    {

        string[] tests = {
            "42 2",
            "2025225 9",
            "239239239 1001",
            "123123 1",
            "1000500 2",
            "1206 20",
            "666333 2",
            "1010 1",
            "8421 4",
            "444222 2"
        };

        Console.WriteLine("--- Запуск тестов ---");
        foreach (var test in tests)
        {
            RunTest(test);
        }
    }

    static void RunTest(string input)
    {
        Console.WriteLine($"Вход: {input}");
        GC.Collect();
        long startMemory = GC.GetTotalMemory(true);
        Stopwatch sw = Stopwatch.StartNew();

        string[] parts = input.Split();
        string s = parts[0];
        BigInteger c = BigInteger.Parse(parts[1]);
        int Ls = s.Length;
        int Lc = parts[1].Length;

        string result = "Не найдено";

        int[] possibleLa = { (Ls + Lc) / 2, (Ls + Lc) / 2 - 1, (Ls + Lc) / 2 + 1 };

        foreach (int La in possibleLa)
        {
            if (La <= 0 || La >= Ls) continue;

            string aStr = s.Substring(0, La);
            string bStr = s.Substring(La);

            if (bStr[0] == '0' && bStr.Length > 1) continue;

            BigInteger a = BigInteger.Parse(aStr);
            BigInteger b = BigInteger.Parse(bStr);

            if (a == b * c)
            {
                result = $"{aStr} {bStr}";
                break;
            }
        }

        sw.Stop();
        long endMemory = GC.GetTotalMemory(false);
        Console.WriteLine($"Ответ: {result}");
        Console.WriteLine($"Время: {sw.Elapsed.TotalMilliseconds:F4} ms");
        Console.WriteLine($"Память: {(endMemory - startMemory) / 1024.0} KB");
        Console.WriteLine("---------------------");
    }
}