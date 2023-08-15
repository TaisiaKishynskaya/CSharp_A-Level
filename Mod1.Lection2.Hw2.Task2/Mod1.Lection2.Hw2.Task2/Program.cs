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
    //var temp = leftNum;
    //leftNum = rightNum;
    //rightNum = temp;
    (leftNum, rightNum) = (rightNum, leftNum);

}

static int[] BubbleSortArr(int[] arrB)
{
    for (var i = 0; i < arrB.Length; i++)
    {
        for (var j = 0; j < arrB.Length - 1; j++)
        {
            if (arrB[j] < arrB[j + 1])
            {
                Swap(ref arrB[j], ref arrB[j + 1]);
            }
        }
    }
    return arrB;
}