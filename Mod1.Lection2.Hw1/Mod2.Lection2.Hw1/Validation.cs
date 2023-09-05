namespace Mod2.Lection2.Hw1
{
    internal class Validation
    {
        static public string ValudateAnswer()
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
    }
}
