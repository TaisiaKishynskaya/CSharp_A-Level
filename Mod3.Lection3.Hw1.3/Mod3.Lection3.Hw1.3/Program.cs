namespace Mod3.Lection3.Hw1._3;

internal class Program
{
    static int[] arr = { 12, 4, 5, 6, 7, 3, 1, 15, 8, 2, 10, 14, 13, 9, 11 };

    static void Main()
    {
        Console.WriteLine("Original array:");
        PrintArray(arr);

        QuickSortAlgorithm(arr, 0, arr.Length - 1);

        Console.WriteLine("\nSorted array:");
        PrintArray(arr);
    }

    public static void QuickSortAlgorithm(int[] arr, int left, int right)
    {
        if (left < right)
        {
            var pivotIndex = Partition(arr, left, right);

            if (pivotIndex > 1)
            {
                QuickSortAlgorithm(arr, left, pivotIndex - 1);
            }

            if (pivotIndex + 1 < right)
            {
                QuickSortAlgorithm(arr, pivotIndex + 1, right);
            }
        }
    }

    public static int Partition(int[] arr, int left, int right)
    {
        var pivot = arr[left];
        while (true)
        {
            while (arr[left] < pivot)
            {
                left++;
            }

            while (arr[right] > pivot)
            {
                right--;
            }

            if (left < right)
            {
                if (arr[left] == arr[right]) return right;

                var temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;
            }
            else
            {
                return right;
            }
        }
    }

    public static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}