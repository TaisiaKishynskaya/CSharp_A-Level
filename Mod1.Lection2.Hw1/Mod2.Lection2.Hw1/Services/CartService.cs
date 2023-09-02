using Mod2.Lection2.Hw1.Models;
using Mod2.Lection2.Hw1.Repositories;

namespace Mod2.Lection2.Hw1.Services;

internal class CartService
{
    private readonly CartRepository _cartRepository; // Здесь сохраняем экземпляр CartRepository

    public CartService(CartRepository cartRepository)
    {
        _cartRepository = cartRepository; // Принимаем переданный экземпляр CartRepository
    }

    public void PrintCartDictionary(Cart cart)
    {
        _cartRepository.AddtoCart(cart);

        Console.WriteLine("All products and prices from cart:");
        foreach (var pr in cart.CartProducts)
        {
            Console.WriteLine($"{pr.Key} cost {pr.Value}$");
        }
    }
}
