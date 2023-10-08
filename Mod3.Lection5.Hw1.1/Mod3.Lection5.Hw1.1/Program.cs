using System.Collections.Concurrent;
using System.Diagnostics;
using static System.Console;


namespace Mod3.Lection5.Hw1._1;

internal class Program
{
    private ConcurrentDictionary<int, string> dictionary = new();

    static void Main()
    {
        var watch = Stopwatch.StartNew();
        Program program = new();

        var input = InputN();

        program.dictionary.TryAdd(input, "Entered number");

        var t1 = new Thread(() => {
            var resultFibonacci = Fibonacci(input);

            program.dictionary.TryAdd(resultFibonacci, "Fibonacci result");

            WriteLine($"\nFibonacci = {resultFibonacci}");
        });

        var t2 = new Thread(() => {
            var resultFactorial = program.Factorial(input);

            program.dictionary.TryAdd(resultFactorial, "Factorial result");

            WriteLine($"Factorial = {resultFactorial}\n");
        });

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        foreach (var kvp in program.dictionary)
        {
            WriteLine($"{kvp.Key} - {kvp.Value}");
        }

        watch.Stop();
        WriteLine($"\nIt took {watch.Elapsed.Seconds} second(s) to complete.");
    }

    public static int InputN()
    {
        while ( true )
        {
            WriteLine("Input positive integer number:");

            var stringInput = ReadLine();

            if (int.TryParse(stringInput, out var nVariable))
            {
                if (nVariable < 0)
                {
                    WriteLine("Input number greater 0.");
                }
                else return nVariable;
            }
            else WriteLine("Input number.");
        }
    }

    public static int Fibonacci(int n)
    {
        if (n == 0)
            throw new ArgumentException("n must be positive number.", nameof(n));

        if (n == 1 || n == 2) 
            return 1;
        
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public int Factorial(int n)
    {
        if (n == 0)
            return 1;

        return n * Factorial(n - 1);
    }
}