using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IProductRepository
{
    Task CreateProductAsync(Product @product);
    Task UpdateProductAsync(Product @product);
    Task DeleteProductAsync(Product @product);
    Task<Product> GetProductAsync(Guid productId);
    Task<IReadOnlyCollection<Product>> GetAllProductsAsync();
    Task AddItems(Guid id, ICollection<OrderItem> orderitems);
}
