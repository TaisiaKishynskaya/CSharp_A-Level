using Mod2.Lection2.Hw1.Models;
using Mod2.Lection2.Hw1.Services;

namespace Mod2.Lection2.Hw1.Repositories;

internal class ProductRepository
{
    public void FillingProductDictionary(Product product)
    {
        while (true)
        {
            Console.Write("Press anyone button.");

            product.ProductName = Console.ReadLine();
            product.Price = ProductService.ValidatePrice();

            if (product.ProductName != null)
            {
                product.dictionaryProductPrice[product.ProductName] = product.Price;
            }

            Console.Write("Continue input? (y/n) ");
            var continueInput = Console.ReadLine();

            if (continueInput != "y") break;
        }
    }
}
