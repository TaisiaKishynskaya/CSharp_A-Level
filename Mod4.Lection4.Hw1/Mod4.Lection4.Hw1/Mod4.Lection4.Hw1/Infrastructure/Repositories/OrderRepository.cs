using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;
using Mod4.Lection4.Hw1.Infrastructure.Context;


namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(EFCoreContext eFContext) : base(eFContext) { }

    public async Task CreateOrderAsync(Order order) => await CreateAsync(order);

    public async Task UpdateOrderAsync(Order order) => await UpdateAsync(order);

    public async Task DeleteOrderAsync(Order order) => await DeleteAsync(order);

    public async Task<IReadOnlyCollection<Order>> GetAllOrdersAsync() => await FindAllAsync().ToListAsync();

    public async Task<Order?> GetOrderAsync(Guid orderId)
    {
        return await FindByConditionAsync(x => x.Id == orderId).FirstOrDefaultAsync();
    }

    public async Task AddItems(Guid id, ICollection<OrderItem> orderitems)
    {
        var order = await GetOrderAsync(id);
        if (order == null) return;
        order.OrderItems = orderitems;
        //await UpdateAsync(school);
    }
}


    /*private readonly EFCoreContext _dbContext;

    public OrderRepository(EFCoreContext dbContext) => _dbContext = dbContext;

    public async Task Insert(Order order)
    {
        await _dbContext.Orderies.AddAsync(order);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Order order)
    {
        _dbContext.Update(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var existingOrder = await _dbContext.Orderies.FindAsync(id);
        if (existingOrder == null) return;
        _dbContext.Remove(existingOrder);
        await _dbContext.SaveChangesAsync();
        /*var tacker = new Order { Id = id };
        _dbContext.Orderies.Attach(tacker);
        _dbContext.Remove(tacker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task InsertManyToMany(int orderId, ICollection<OrderItem> orderitems)
    {
        // Option 1: find, update
        var existingOrder = await _dbContext.Orderies.FindAsync(orderId);
        if (existingOrder == null) return;

        existingOrder.OrderItems = orderitems;
        _ = await _dbContext.SaveChangesAsync();
    }*/
