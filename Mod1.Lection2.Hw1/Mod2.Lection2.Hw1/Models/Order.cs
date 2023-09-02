using Mod2.Lection2.Hw1.Services;

namespace Mod2.Lection2.Hw1.Models;

internal class Order
{
    private string? _pb;
    public Dictionary<string, int> OrderProducts { get; } = new();

    public string? ProductToBuy
    {
        get { return _pb; }
        set
        {
            value = ProductService.ValidateName();
            _pb = value;
        }
    }
}
