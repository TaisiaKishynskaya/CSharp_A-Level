using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

static internal class ExtensionMethod
{
    static public List<ISweetnessLook> FilterAndPrint(this List<ISweetnessLook> list)
    {
        Console.Write("Enter minimum weight for filtering: ");
        if (!double.TryParse(Console.ReadLine(), out var minWeight))
        {
            Console.WriteLine("Invalid input. Filtering canceled.");
            return new List<ISweetnessLook>();
        }

        var filteredList = list.Where(s => s.Weight >= minWeight).ToList();

        if (filteredList.Count == 0)
        {
            Console.WriteLine("No items match the specified criteria.");
        }
        else
        {
            Console.WriteLine("Filtered list:");
            for (var i = 0; i < filteredList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredList[i].Name} - {filteredList[i].Weight} gr");
            }
        }

        return filteredList;
    }

}
