namespace Mod3.Lection3.Hw1._2;

internal class Program
{
    static int[] sortedArray = { 1, 3, 5, 7, 9, 11, 13, 15 };
    static int target = 7;

    static void Main()
    {
        var result = BinarySearch(sortedArray, target);

        PrintResult(result);
    }

    public static int BinarySearch(int[] sortedArray, int target)
    {
        var left = 0;
        var right = sortedArray.Length - 1;

        while (left <= right)
        {
            var middle = left + (right - left) / 2;

            // Если элемент посередине массива равен целевому значению, возвращаем его индекс
            if (sortedArray[middle] == target)
            {
                return middle;
            }
            // Если целевое значение меньше элемента в середине, ищем в левой половине
            else if (sortedArray[middle] > target)
            {
                right = middle - 1;
            }
            // Если целевое значение больше элемента в середине, ищем в правой половине
            else
            {
                left = middle + 1;
            }
        }

        // Если элемент не найден, возвращаем -1
        return -1;
    }

    public static void PrintResult(int result)
    {
        if (result != -1)
        {
            Console.WriteLine($"Element {target} found at index {result}");
        }
        else
        {
            Console.WriteLine($"Element {target} not found in the array");
        }
    }
}