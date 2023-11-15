using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;
using Mod4.Lection4.Hw1.Infrastructure.Context;

namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(EFCoreContext eFContext) : base(eFContext) { }

    public async Task CreateOrderItemAsync(OrderItem orderitem) => await CreateAsync(orderitem);

    public async Task UpdateOrderItemAsync(OrderItem orderitem) => await UpdateAsync(orderitem);

    public async Task DeleteOrderItemAsync(OrderItem orderitem) => await DeleteAsync(orderitem);

    public async Task<IReadOnlyCollection<OrderItem>> GetAllItemsAsync() => await FindAllAsync().ToListAsync();

    public async Task<OrderItem?> GetOrderItemAsync(Guid productId)
    {
        return await FindByConditionAsync(x => x.Id == productId).FirstOrDefaultAsync();
    }
}


/*private readonly EFCoreContext _dbContext;

    public OrderItemRepository(EFCoreContext dbContext) => _dbContext = dbContext;

    public async Task Insert(OrderItem orderItem)
    {
        await _dbContext.OrderItemes.AddAsync(orderItem);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task Update(OrderItem orderItem)
    {
        _dbContext.Update(orderItem);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var tacker = new OrderItem { Id = id };
        _dbContext.OrderItemes.Attach(tacker);
        _dbContext.Remove(tacker);
        await _dbContext.SaveChangesAsync();
    }*/
