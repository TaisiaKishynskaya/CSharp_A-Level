using System;
using System.Collections.Generic;
using System.Linq;
namespace Mod2.Lection3.Hw1;

internal class ClientAnswer
{
    public static string? GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    public static bool ContinueInput()
    {
        var continueInput = GetInput("Continue input? (y/n) ");
        return continueInput == "y";
    }

    static public string ValidateAnswer()
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (input == "y" || input == "n" || input == "e")
            {
                return input;
            }
            else
            {
                Console.WriteLine("Input y or n!");
            }
        }
    }
}
