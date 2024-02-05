using Microsoft.EntityFrameworkCore;
using Ordering.Core.Abstractions.Repositories;
using Ordering.DataAccess.Entities;
using Ordering.DataAccess.Infrastructure;

namespace Ordering.DataAccess.Repositories;

public class OrderRepository : IOrderRepository<OrderEntity>
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderEntity>> Get(int page, int size)
    {
        IQueryable<OrderEntity> query = _dbContext.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .AsNoTracking()
            .OrderBy(order => order.Id);

        if (page > 0 && size > 0)
        {
            query = query
                .Skip((page - 1) * size)
                .Take(size);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<OrderEntity>> GetByUser(string userId, int page, int size)
    {
        IQueryable<OrderEntity> query = _dbContext.Orders
            .Where(order => order.UserId == userId)
            .Include(order => order.User)
            .Include(order => order.Items)
            .AsNoTracking()
            .OrderBy(order => order.Id);

        if (page > 0 && size > 0)
        {
            query = query
                .Skip((page - 1) * size)
                .Take(size);
        }

        return await query.ToListAsync();
    }

    public async Task<OrderEntity> GetById(int id)
    {
        return await _dbContext.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<OrderEntity> Add(OrderEntity order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<OrderEntity> Update(OrderEntity order)
    {
        //_dbContext.Orders.Update(order);
        _dbContext.Entry(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<OrderEntity> Delete(int id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        _dbContext.Remove(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }
}
