using Mod2.Lection2.Hw1.Models;
using Mod2.Lection2.Hw1.Repositories;

namespace Mod2.Lection2.Hw1.Services;

internal class ProductService
{
    static public string ValidateName()
    {
        while (true)
        {
            Console.WriteLine("Input product: ");
            var inputProduct = Console.ReadLine();

            if (inputProduct != null && inputProduct.All(c => char.IsLetter(c) || " ".Contains(c)))
            {
                return inputProduct;
            }
        }
    }

    public static int ValidatePrice()
    {
        while (true)
        {
            Console.WriteLine("Input price of this product in dollars: ");
            var inputPrice = Console.ReadLine();

            if (inputPrice != null && int.TryParse(inputPrice, out var parsedPrice))
            {
                if (parsedPrice <= 0)
                {
                    Console.WriteLine("Price should be greater 0.");
                }
                else return parsedPrice;
            }
            else
            {
                Console.WriteLine("Input only number.");
            }
        }
    }

    public void PrintDictionaryOfAllProducts(Product product)
    {
        var products = new ProductRepository();

        products.FillingProductDictionary(product);

        Console.WriteLine("All products and prices:");
        foreach (var pr in product.dictionaryProductPrice)
        {
            Console.WriteLine($"{pr.Key} cost {pr.Value}$");
        }
    }
}
