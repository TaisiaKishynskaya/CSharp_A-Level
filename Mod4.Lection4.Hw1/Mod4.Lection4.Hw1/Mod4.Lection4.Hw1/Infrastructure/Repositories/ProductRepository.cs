using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;
using Mod4.Lection4.Hw1.Infrastructure.Context;

namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(EFCoreContext eFContext) : base(eFContext) { }

    public async Task CreateProductAsync(Product product) => await CreateAsync(product);

    public async Task UpdateProductAsync(Product product) => await UpdateAsync(product);

    public async Task DeleteProductAsync(Product product) => await DeleteAsync(product);

    public async Task<IReadOnlyCollection<Product>> GetAllProductsAsync() => await FindAllAsync().ToListAsync();

    public async Task<Product?> GetProductAsync(Guid productId)
    {
        return await FindByConditionAsync(x => x.Id == productId).FirstOrDefaultAsync();
    }

    public async Task AddItems(Guid id, ICollection<OrderItem> orderitems)
    {
        var productItem = await GetProductAsync(id);
        if (productItem == null) return;
        productItem.ProductItems = orderitems;
        //await UpdateAsync(school);
    }
}



/*private readonly EFCoreContext _dbContext;

    public ProductRepository(EFCoreContext dbContext) => _dbContext = dbContext;

    public async Task Insert(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _dbContext.Update(product);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existingProduct = await _dbContext.Products.FindAsync(id);
        if (existingProduct == null) return;
        _dbContext.Remove(existingProduct);
        await _dbContext.SaveChangesAsync();
        /*var tacker = new Product { Id = id };
        _dbContext.Products.Attach(tacker);
        _dbContext.Remove(tacker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task InsertManyToMany(int productId, ICollection<OrderItem> orderitems)
    {
        // Option 1: find, update
        var existingProduct = await _dbContext.Products.FindAsync(productId);
        if (existingProduct == null) return;

        existingProduct.ProductItems = orderitems;
        _ = await _dbContext.SaveChangesAsync();
    }
    */
