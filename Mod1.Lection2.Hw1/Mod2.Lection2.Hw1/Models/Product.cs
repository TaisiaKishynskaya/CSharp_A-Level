using Mod2.Lection2.Hw1.Services;

namespace Mod2.Lection2.Hw1.Models;

public class Product
{
    private string? _product;
    public Dictionary<string, int> dictionaryProductPrice = new();

    public string? ProductName 
    { 
        get { return _product; }
        set
        {
            value = ProductService.ValidateName();
            _product = value;
        } 
    }
    public int Price { get; set; }
}
