namespace Mod2.Lection3.Hw1;

internal class CandyWithFilling : Candy
{
    public string? Filling { get; set; }

    public CandyWithFilling(string name, double weight, string taste, string color, string sugar, string chocolate, string filling, string wrapperColor)
        : base(name, weight, taste, color, sugar, chocolate, wrapperColor)
    {
        Filling = filling;
    }
}
