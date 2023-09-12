using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

internal class Candy : Sugar
{
    public string? ChocolateType { get; set; }

    public Candy(string name, double weight, string taste, string color, string sugar, string chocolate, string wrapperColor)
        : base(name, weight, taste, color, sugar, wrapperColor)
    {
        ChocolateType = chocolate;
    }
}
