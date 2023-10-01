namespace Mod3.Lection2.Hw2._1_12;

internal class Program
{
    readonly int[] numbers = { 1, 2, 3, 100, -10, 22, 36, 893, 13, 104, 33, 1, 3, -10, 22, 22, 36, 13, 1, 2 };
    readonly string inputString = "Abilities forfeited situation extremely my to he resembled. Old had conviction discretion understood put principles you.";
    readonly List<int> nums = new() { 1, 2, 3, 100, -10, 22, 36, 893, 13, 104, 33, 1, 3, -10, 22, 22, 36, 13, 1, 2 };
    readonly List<string> words = new() { "Abilities", "forfeited", "situation", "extremely", "my", "to", "he", "resembled", "Old",
                                          "had", "conviction", "discretion", "understood", "put", "principles", "you" };

    static void Main()
    {
        Program program = new();

        program.FindNumsDivisionBy2And3();
        program.FindSum();
        program.FindAverage();
        program.FindMaxNumber();
        program.FindMinNumber();
        program.MultiplyNumsGreater10By10();
        program.FindNumbersAdFrecuency();
        program.GroupIntoEvenAndOdd();
        program.FindNumbersGreaterAverage();

        program.FindUniqueCharachters();

        program.GroupByLengthOfTheString();
        program.NormalizeGroupedStringsWithSubstring();
    }

    private void FindNumsDivisionBy2And3()
    {
        var divisibleBy2And3 = numbers.Where(n => n % 2 == 0 && n % 3 == 0);

        Console.WriteLine("Numbers divisible by 2 and 3:");
        foreach (var number in divisibleBy2And3)
        {
            Console.WriteLine(number);
        }
    }

    private void FindSum()
    {
        var sum = numbers.Sum();

        Console.WriteLine($"Sum of all numbers: {sum}");
    }

    private void FindAverage()
    {
        var average = numbers.Average();

        Console.WriteLine("Average of all numbers: " + average);
    }

    private void FindMaxNumber()
    {
        var maxNumber = numbers.Max();

        Console.WriteLine("Maximum number: " + maxNumber);
    }

    private void FindMinNumber()
    {
        var minNumber = numbers.Min();

        Console.WriteLine("Minimum number: " + minNumber);
    }

    private void MultiplyNumsGreater10By10()
    {
        var result = numbers.Where(n => n > 10).Select(n => n * 10);

        Console.WriteLine("Numbers greater than 10, multiplied by 10:");
        foreach (var number in result)
        {
            Console.WriteLine(number);
        }
    }

    private void FindUniqueCharachters()
    {
        var uniqueCharacters = inputString.Distinct();

        Console.WriteLine("Unique characters in the string:");
        foreach (var character in uniqueCharacters)
        {
            Console.WriteLine(character);
        }
    }

    private void FindNumbersAdFrecuency()
    {
        var numberFrequency = numbers
            .GroupBy(n => n)
            .Select(group => new { Number = group.Key, Frequency = group.Count() });

        Console.WriteLine("Number\tFrequency");
        foreach (var item in numberFrequency)
        {
            Console.WriteLine($"{item.Number}\t{item.Frequency}");
        }
    }

    private void GroupIntoEvenAndOdd()
    {
        var groupedNumbers = numbers
            .GroupBy(n => n % 2 == 0 ? "Even" : "Odd")
            .Select(group => new
            {
                Group = group.Key,
                MaxNumber = group.Max()
            });

        foreach (var group in groupedNumbers)
        {
            Console.WriteLine($"{group.Group} Group: Max Number = {group.MaxNumber}");
        }
    }

    private void FindNumbersGreaterAverage()
    {
        var average = nums.Average();

        var greaterThanAverage = numbers.Where(num => num > average);

        Console.WriteLine("Elements greater than the average:");
        foreach (var num in greaterThanAverage)
        {
            Console.WriteLine(num);
        }
    }

    private void GroupByLengthOfTheString()
    {
        var groupedByLength = words.GroupBy(word => word.Length);

        foreach (var group in groupedByLength)
        {
            Console.WriteLine($"Words with length {group.Key}:");
            foreach (var word in group)
            {
                Console.WriteLine(word);
            }
        }
    }

    private void NormalizeGroupedStringsWithSubstring()
    {
        var targetSubstring = "ed"; // The target substring to search for

        var filteredAndNormalized = words
            .Where(word => word.Contains(targetSubstring))
            .GroupBy(word => word.Length)
            .Select(group => group
                .Select(word => NormalizeString(word))
                .ToList())
            .ToList();

        Console.WriteLine($"Words containing '{targetSubstring}' grouped by length and normalized:");

        foreach (var group in filteredAndNormalized)
        {
            Console.WriteLine($"Length {group.First().Length}: {string.Join(", ", group)}");
        }

        static string NormalizeString(string input)
        {
            // Normalize the string (first character upper-case, others lower-case)
            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }
    }
}