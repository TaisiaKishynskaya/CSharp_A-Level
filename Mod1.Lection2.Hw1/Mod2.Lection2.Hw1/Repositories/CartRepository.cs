using Mod2.Lection2.Hw1.Models;

namespace Mod2.Lection2.Hw1.Repositories;

internal class CartRepository
{
    private readonly Product _product; // В конструкторе принимаем заполненный экземпляр Product
    public CartRepository(Product product)
    {
        _product = product; // Сохраняем переданный экземпляр Product
    }

    public void AddtoCart(Cart cart)
    {
        while (true)
        {
            Console.WriteLine("Time to shop! Press any button.");
            cart.SelectedProduct = Console.ReadLine(); // Здесь устанавливаем SelectedProduct через свойство

            if (_product.dictionaryProductPrice.TryGetValue(cart.SelectedProduct, out var price))
            {
                if (cart.CartProducts.Count >= 10)
                {
                    Console.WriteLine("You cannot add more than 10 products to the cart.");
                }
                else
                {
                    if (cart.CartProducts.ContainsKey(cart.SelectedProduct))
                    {
                        cart.CartProducts[cart.SelectedProduct] += price; // Увеличиваем цену, если товар уже есть в корзине
                    }
                    else
                    {
                        cart.CartProducts[cart.SelectedProduct] = price; // Добавляем новый товар в корзину
                    }

                    Console.WriteLine($"{cart.SelectedProduct} has been added to the cart.");
                }
            }
            else
            {
                Console.WriteLine("This product does not exist.");
            }

            Console.Write("Continue input? (y/n) ");
            var continueInput = Console.ReadLine();

            if (continueInput != "y") break;
        }
    }
}
