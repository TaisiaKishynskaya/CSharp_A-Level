using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

internal class Cookie : Flour
{
    public string? Dough { get; set; }

    public Cookie(string name, double weight, string taste, string color, string flour, string dough, string wrapperColor)
        : base(name, weight, taste, color, flour, wrapperColor)
    {
        Dough = dough;
    }
}
