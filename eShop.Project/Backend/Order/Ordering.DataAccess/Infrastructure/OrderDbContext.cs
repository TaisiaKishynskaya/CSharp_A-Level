using Microsoft.EntityFrameworkCore;
using Ordering.DataAccess.Configurations;
using Ordering.DataAccess.Entities;

namespace Ordering.DataAccess.Infrastructure;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
        
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<OrderItemEntity> Items { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new OrderItemConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
    }
}
