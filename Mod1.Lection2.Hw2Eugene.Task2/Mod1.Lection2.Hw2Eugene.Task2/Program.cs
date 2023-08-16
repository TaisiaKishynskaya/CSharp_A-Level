Console.WriteLine("Enter the size of the Array:");

var number = TryParseMethod();
var arr = new int[number];

int max, min;
FillingArray();

SearchEvenOddNums();
SearchMinMaxNums();


static int TryParseMethod()
{
    while (true) 
    {
        var input = Console.ReadLine();

        if (input != null && int.TryParse(input, out var num))
        {
            Console.WriteLine($"Your input: {num}");
            return num;
        }
        else 
        { 
            Console.WriteLine("Input integer number!");
        }
    }
}

void InputMaxMin()
{
    Console.WriteLine("Input min value:");
    min = TryParseMethod();

    Console.WriteLine("Input max value:");
    max = TryParseMethod();
}

void FillingArray()
{
    var random = new Random();

    InputMaxMin();

    for (var i = 0; i < arr.Length; i++)
    {
        arr[i] = random.Next(min, max);
        Console.WriteLine(arr[i]);
    }
}

void SearchEvenOddNums()
{
    var countEven = 0;
    var countOdd = 0;

    foreach (var i in arr)
    {
        if (i == 0)
        {
            continue;
        }
        else if (i % 2 == 0)
        {
            countEven++;
        }
        else
        {
            countOdd++;
        }
    }

    Console.WriteLine($"Quntity of even numbers: {countEven}"); 
    Console.WriteLine($"Quntity of odd numbers: {countOdd}");
}

void SearchMinMaxNums()
{
    for (var i = 0; i < arr.Length; i++)
    {
        for (var j = 0; j < arr.Length - 1; j++)
        {
            if (arr[j] < arr[j + 1])
            {
                (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
            }
        }
    }
    Console.WriteLine($"Largest element: {arr[0]}");
    Console.WriteLine($"Smallest element: {arr[number - 1]}");
}
