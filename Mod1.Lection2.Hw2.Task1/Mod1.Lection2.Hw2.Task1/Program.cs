namespace Mod1.Lection2.Hw2.Task1
{
    internal class Program
    {
        public int nVariable;
        public int count = 0;

        static void Main()
        {
            var program = new Program();

            program.TryParseMethod();
            program.Counting();

            Console.WriteLine($"Quantity of numbers in [-100; 100] = {program.count}");
        }

        private void TryParseMethod() 
        {
            while (true)
            {
                Console.WriteLine("Input: N = ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out nVariable))
                {
                    if (nVariable <= 0)
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

        private void Counting()
        {
            var arr = new int[nVariable];
            var random = new Random();

            foreach (var i in arr)
            {
                arr[i] = random.Next(-200, 200);
                Console.WriteLine(arr[i]);

                if (arr[i] >= -100 & arr[i] <= 100)
                {
                    count++;
                }
            }
        }
    }
}
