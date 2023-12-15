namespace Catalog.Host;

public class Product
{
    public DateTime Date { get; set; }

    public int PriceWithDiscont { get; set; }

    public int Price => (int)(PriceWithDiscont / 0.5);

    public string? Summary { get; set; }
}