using Mod2.Lection3.Hw1.Models;

namespace Mod2.Lection3.Hw1;

internal class Sweetness : ISweetnessLook
{
    public string? Name { get; set; }

    public double Weight { get; set; }
    public string? Taste { get; set; }
    public string? Color { get; set; }
    public string? WrapperColor { get; set; }



    public Sweetness(string name, double weight, string taste, string color, string wrapperColor)
    {
        Name = name;
        Weight = weight;
        Taste = taste;
        Color = color;
        WrapperColor = wrapperColor;
    }
}
