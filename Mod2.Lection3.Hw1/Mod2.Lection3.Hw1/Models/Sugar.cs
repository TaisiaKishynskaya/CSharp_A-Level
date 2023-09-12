namespace Mod2.Lection3.Hw1;

internal class Sugar : Sweetness
{
    public string? SugarType { get; set; }

    public Sugar(string name, double weight, string taste, string color, string sugar, string wrapperColor) 
        : base(name, weight, taste, color, wrapperColor)
    {
        SugarType = sugar;
    }
}
