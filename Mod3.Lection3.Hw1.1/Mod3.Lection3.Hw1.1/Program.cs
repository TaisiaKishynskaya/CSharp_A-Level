namespace Mod3.Lection3.Hw1._1;

internal class Program
{
    static void Main()
    {
        var resultFibonacci = Fibonacci(5);
        Console.WriteLine($"Fibonacci = {resultFibonacci}");

        var resultFactorial = Factorial(4);
        Console.WriteLine($"Factorial = {resultFactorial}");
    }

    public static int Fibonacci(int n)
    {
        if (n <= 0)
            throw new ArgumentException("n must be positive number.", nameof(n));

        if (n == 1 || n == 2)
            return 1;

        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public static int Factorial(int n)
    {
        if (n < 0)
            throw new ArgumentException("n must be not negative number.", nameof(n));

        if (n == 0)
            return 1;

        return n * Factorial(n - 1);
    }
}