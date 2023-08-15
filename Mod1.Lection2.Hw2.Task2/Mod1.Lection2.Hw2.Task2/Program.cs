var arrA = new int[20];
var arrB = new int[20];

FillingArrays();
Console.WriteLine($"Sorted array {string.Join(",", BubbleSortArr(arrB))}");

void FillingArrays()
{
    var random = new Random();

    for (var i = 0; i < arrA.Length; i++)
    {
        arrA[i] = random.Next(0, 1000);
        Console.WriteLine($"Arr A, element {i}: {arrA[i]}");

        var arrElement = arrA[i] <= 888 ? arrB[i] = arrA[i] : arrA[i] = random.Next(0, 888);
        Console.WriteLine($"Arr B, element {i}: {arrElement}");
    }
}

static void Swap(ref int leftNum, ref int rightNum)
{
    var temp = leftNum;
    leftNum = rightNum;
    rightNum = temp;
}

static int[] BubbleSortArr(int[] arrB)
{
    for (var j = 0; j < arrB.Length; j++)
    {
        for (var i = 0; i < arrB.Length - 1; i++)
        {
            if (arrB[i] < arrB[i + 1])
            {
                Swap(ref arrB[i], ref arrB[i + 1]);
            }
        }
    }
    return arrB;
}