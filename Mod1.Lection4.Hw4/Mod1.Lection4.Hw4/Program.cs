namespace Lection3.Task1
{
    internal class Program
    {
        private string? inputN;

        static void Main()
        {
            var program = new Program();

            Console.WriteLine("Input: N = ");
            var variableN = 0;
            program.InputN(ref variableN);

            var firstArr = new int[variableN];
            var evenNumList = new List<int>();
            var oddNumList = new List<int>();

            program.FillArrays(firstArr, evenNumList, oddNumList);

            var evenNumArr = evenNumList.ToArray();
            var oddNumArr = oddNumList.ToArray();

            program.PrintNumArrays(evenNumArr, oddNumArr);
            program.ChangeNumOnLetters(ref evenNumArr, ref oddNumArr);
        }

        private void InputN(ref int inputResult)
        {
            while (true)
            {
                inputN = Console.ReadLine();

                if (int.TryParse(inputN, out inputResult))
                {
                    if (inputResult <= 0)
                    {
                        Console.WriteLine("Input number greater 0.");
                    }
                    else break;
                }
                else
                {
                    Console.WriteLine("Input number.");
                }
            }
        }

        private void FillArrays(int[] firstArr, List<int> evenNumList, List<int> oddNumList)
        {
            var random = new Random();

            for (var i = 0; i < firstArr.Length; i++)
            {
                firstArr[i] = random.Next(1, 26);
                Console.WriteLine($"1st Arr, element {i}: {firstArr[i]}");

                switch (firstArr[i] % 2)
                {
                    case 0:
                        evenNumList.Add(firstArr[i]);
                        break;
                    case 1:
                        oddNumList.Add(firstArr[i]);
                        break;
                }
            }
        }

        private void PrintNumArrays(int[] evenNumArr, int[] oddNumArr)
        {
            var numArrays = new Dictionary<string, int[]>() { { "Even", evenNumArr }, { "Odd", oddNumArr } };
            var keys = numArrays.Keys.ToArray();

            foreach (var key in keys)
            {
                var array = numArrays[key];

                foreach (var num in array)
                {
                    Console.WriteLine($"{key} Arr, element = {num}");
                }
            }
        }

        private void ChangeNumOnLetters(ref int[] evenNumArr, ref int[] oddNumArr)
        {
            var dictionary = new Dictionary<int, char>()
            { { 0, 'A' }, { 1, 'b' }, { 2, 'c' }, { 3, 'D' }, { 4, 'E' }, { 5, 'f' },
                { 6, 'g' }, { 7, 'H' }, { 8, 'i' }, { 9, 'J' }, { 10, 'k' },
                { 11, 'L' }, { 12, 'm' }, { 13, 'n' }, { 14, 'o' }, { 15, 'p' },
                { 16, 'q' }, { 17, 'r' }, { 18, 's' }, { 19, 't' }, { 20, 'u' },
                { 21, 'v' }, { 22, 'w' }, { 23, 'x' }, { 24, 'y' }, { 25, 'z' } };

            var charEvenArr = new char[evenNumArr.Length];
            var charOddArr = new char[oddNumArr.Length];

            for (var i = 0; i < evenNumArr.Length; i++)
            {
                charEvenArr[i] = (char)evenNumArr[i];
            }

            for (var i = 0; i < evenNumArr.Length; i++)
            {
                if (dictionary.ContainsKey(charEvenArr[i]))
                {
                    charEvenArr[i] = dictionary[charEvenArr[i]];
                }
            }

            Console.WriteLine(string.Join(", ", charEvenArr));

            for (var i = 0; i < charOddArr.Length; i++)
            {
                charOddArr[i] = (char)oddNumArr[i];
            }

            for (var i = 0; i < oddNumArr.Length; i++)
            {
                if (dictionary.ContainsKey(charOddArr[i]))
                {
                    charOddArr[i] = dictionary[charOddArr[i]];
                }
            }

            Console.WriteLine(string.Join(", ", charOddArr));
        }
    }
}