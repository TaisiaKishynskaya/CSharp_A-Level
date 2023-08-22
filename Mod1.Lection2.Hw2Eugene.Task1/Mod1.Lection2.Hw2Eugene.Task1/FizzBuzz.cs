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
            Console.WriteLine($"{arr[i]} is FizzBuzz");
        }
        else if(arr[i] % 5 == 0) 
        {
            Console.WriteLine($"{arr[i]} is Buzz");
        } 
        else if (arr[i] % 3 == 0) 
        {
            Console.WriteLine($"{arr[i]} is Fizz");
        }
        else 
        {
            Console.WriteLine($"{arr[i]} isn't Fizz/Buzz/FizzBuzz");
        }
    }
}
