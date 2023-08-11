using System;
using UtilityLibraries;

namespace Calculator
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter 1st number: ");
            var firstInput = Console.ReadLine();
            var firstNum = TryParseMethod(firstInput);

            Console.WriteLine("Enter 2nd number: ");
            var secondInput = Console.ReadLine();
            var secondNum = TryParseMethod(secondInput);

            Console.WriteLine($"{firstNum} + {secondNum} = {CalculatorLib.Add(firstNum, secondNum)}.");
            Console.WriteLine($"{firstNum} - {secondNum} = {CalculatorLib.Subtraction(firstNum, secondNum)}.");
            Console.WriteLine($"{firstNum} * {secondNum} = {CalculatorLib.Multiplication(firstNum, secondNum)}.");
            Console.WriteLine($"{firstNum} / {secondNum} = {CalculatorLib.Division(firstNum, secondNum)}.");
        }
        
        public static int TryParseMethod(string input) 
        {
            if (input != null && int.TryParse(input, out var number))
            {
                Console.WriteLine($"Your input: {number}");
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input.");
                return 0;
            }
        }
    }
}