using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

internal class Gift
{
    static public List<ISweetnessLook> sweets = new() 
    {
        new Candy("COFFEE", 8, "coffee", "brown", "beet sugar", "dark", "gold"),
        new Candy("CHAMOMILE", 8, "milk chocolate", "brown", "cane sugar", "milk", "silver"),
        new CandyWithFilling("RAFAELLO", 13.5, "coconut", "white", "cane sugar", "light", "coconut cream", "silver"),
        new CandyWithFilling("DRUNKEN CHERRY", 14.5, "liquor", "brown", "cane sugar", "dark", "liquor", "red"),
        new Jelly("CRAZY BEE", 6, "mango", "orange", "beet sugar", "agar-agar", "orange"),
        new Jelly("FRUITTY", 6, "apple", "green", "beet sugar", "gelatin", "green"),
        new Cookie("MARIA", 10.5, "vanilla", "orange", "wheat flour", "unleavened dough", "blue"),
        new Cookie("OREO", 21.5, "cacao", "brown", "corn flour", "shortbread dough", "yellow"),
    };
    public List<ISweetnessLook> giftSweetness = new() { };

    public string? SelectedSweet { get; set; }

    static public void Start()
    {
        var gift = new Gift();

        Gift.PrintSweetsList();

        gift.AddToGift();
        gift.PrintGift();
        gift.Menu();
    }

    public void Menu()
    {
        Console.WriteLine("Do you want to find sweetness? (y/n)");
        var answer = ClientAnswer.ValidateAnswer();

        switch (answer)
        {
            case "y":
                // Вызываем метод без аргументов, он запросит ввод минимального веса у пользователя
                _ = giftSweetness.FilterAndPrint();
                break;

            case "n":
                return;
        }
    }

    private static List<ISweetnessLook> BubbleSortArr(List<ISweetnessLook> giftSweetness)
    {
        for (var j = 0; j < giftSweetness.Count; j++)
        {
            for (var i = 0; i < giftSweetness.Count - 1; i++)
            {
                if (giftSweetness[i].Weight < giftSweetness[i + 1].Weight)
                {
                    (giftSweetness[i].Weight, giftSweetness[i + 1].Weight) = (giftSweetness[i + 1].Weight, giftSweetness[i].Weight);
                    (giftSweetness[i].Name, giftSweetness[i + 1].Name) = (giftSweetness[i + 1].Name, giftSweetness[i].Name);
                }
            }
        }
        return giftSweetness;
    }

    public void AddToGift()
    {
        while (true)
        {
            Console.WriteLine("Collect a gift! Input sweetness name to add it to gift:");
            SelectedSweet = Console.ReadLine();

            if (SelectedSweet != null)
            {
                SelectedSweet = SelectedSweet.ToUpper();

                if (SelectedSweet != null && sweets.Exists(s => s.Name == SelectedSweet))
                {
                    var selectedSweet = sweets.First(s => s.Name == SelectedSweet);
                    giftSweetness.Add(selectedSweet);
                    var totalWeight = giftSweetness.Sum(s => s.Weight);
                    Console.WriteLine($"{SelectedSweet} has been added to the gift.");
                    Console.WriteLine($"Total weight of the gift: {totalWeight}");
                }
                else
                {
                    Console.WriteLine("This sweetness does not exist.");
                }
            }

            if (!ClientAnswer.ContinueInput()) break;
        }
    }

    public void PrintGift()
    {
        Console.WriteLine("Your sorted by weight gift:");
        var sortedGift = BubbleSortArr(giftSweetness); // Получаем отсортированный список
        for (var i = 0; i < sortedGift.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {sortedGift[i].Name} - {sortedGift[i].Weight} gr");
        }

        //Console.WriteLine($"Sorted sweetness by weight: {string.Join(",", BubbleSortArr(giftSweetness))}");
    }

    static public void PrintSweetsList()
    {
        for (var i = 0; i < sweets.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Name: {sweets[i].Name}, Weight: {sweets[i].Weight} gr, Taste: {sweets[i].Taste}, Color: {sweets[i].Color}, " +
                $"Wrapper color: {sweets[i].WrapperColor}");

            if (sweets[i] is Cookie cookie)
            {
                Console.WriteLine($"Flour: {cookie.FlourType}, Dough: {cookie.Dough}");
            }
            else if (sweets[i] is Jelly jelly)
            {
                Console.WriteLine($"Sugar: {jelly.SugarType}, Chocolate: {jelly.GellingAgents}");
            }
            else if (sweets[i] is Candy candy)
            {
                // Объект является экземпляром класса Candy, можно использовать свойства Candy
                Console.WriteLine($"Sugar: {candy.SugarType}, Chocolate: {candy.ChocolateType}");
            }
            else if (sweets[i] is CandyWithFilling candyWithFilling)
            {
                Console.WriteLine($"Sugar: {candyWithFilling.SugarType}, Chocolate: {candyWithFilling.ChocolateType}, Filling: {candyWithFilling.Filling}");
            }
        }
    }

}
