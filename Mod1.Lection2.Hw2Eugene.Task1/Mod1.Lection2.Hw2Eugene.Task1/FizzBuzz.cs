using System;

var arr = new int[100];

FillingArray();

void FillingArray() 
{
    for (var i = 0; i < arr.Length; i++) 
    {
        arr[i] = i + 1;

        if (arr[i] % 3 == 0 && arr[i] % 5 == 0)
        {
            Console.WriteLine($"{arr[i] = i + 1} is FizzBuzz");
        }
        else if(arr[i] % 5 == 0) 
        {
            Console.WriteLine($"{arr[i] = i + 1} is Buzz");
        } 
        else if (arr[i] % 3 == 0) 
        {
            Console.WriteLine($"{arr[i] = i + 1} is Fizz");
        }
        else 
        {
            Console.WriteLine($"{arr[i] = i + 1} isn't Fizz/Buzz/FizzBuzz");
        }
    }
}
