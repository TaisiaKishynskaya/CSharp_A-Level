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
            cart.SelectedProduct = Console.ReadLine(); // Здесь устанавливаем SelectedProduct через свойство

            if (cart.SelectedProduct != null && _product.dictionaryProductPrice.TryGetValue(cart.SelectedProduct, out var price))
            {
                if (cart.CartProducts.Count >= 10)
                {
                    Console.WriteLine("You cannot add more than 10 products to the cart.");
                }
                else
                {
                    cart.CartProducts[cart.SelectedProduct] = cart.CartProducts.ContainsKey(cart.SelectedProduct)
                        ? cart.CartProducts[cart.SelectedProduct] + price
                        : price;

                    Console.WriteLine($"{cart.SelectedProduct} has been added to the cart.");
                }
            }
            else
            {
                Console.WriteLine("This product does not exist.");
            }

            if (!Validation.ContinueInput()) break;
        }
    }
}
