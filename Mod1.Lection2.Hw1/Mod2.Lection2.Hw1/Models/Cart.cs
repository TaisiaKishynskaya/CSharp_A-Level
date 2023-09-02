using Mod2.Lection2.Hw1.Services;

namespace Mod2.Lection2.Hw1.Models;

public class Cart
{
    private string? _ps;
    public Dictionary<string, int> CartProducts { get; } = new();

    public string? SelectedProduct
    {
        get { return _ps; }
        set
        {
            value = ProductService.ValidateName();
            _ps = value;
        }
    }
}

