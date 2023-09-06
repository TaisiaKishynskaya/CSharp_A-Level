namespace Mod2.Lection3.Hw1;

internal class Flour : Sweetness
{
    public string? FlourType { get; set; }

    public Flour(string name, double weight, string taste, string color, string flour, string wrapperColor) 
        : base (name, weight, taste, color, wrapperColor)
    {
        FlourType = flour;
    }
}
