namespace ModuleWork3._1;

internal class InputValidation
{
    internal static string InputString()
    {
        while (true)
        {
            Console.WriteLine("Input name:");

            var inputValue = Console.ReadLine();

            if (inputValue != null && inputValue != "" && inputValue.All(char.IsLetter))
            {
                return inputValue;
            }
            else
            {
                Console.WriteLine("Input only letters!");
            }
        }
    }

    internal static uint InputNumber()
    {
        while (true)
        {
            Console.WriteLine($"Input phone number:");

            var inputValue = Console.ReadLine();
            if (inputValue != null && inputValue != "" && uint.TryParse(inputValue, out var number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Input only nums!");
            }
        }
    }

    internal static string InputChoise()
    {
        while (true)
        {
            var inputValue = Console.ReadLine();

            if (inputValue == "1" || inputValue == "2" || inputValue == "3")
            {
                return inputValue;
            }
            else
            {
                Console.WriteLine("Input only nums!");
            }
        }
    }
}
