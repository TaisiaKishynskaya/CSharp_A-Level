using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Infrastructure.Context;

public class EFCoreContext : DbContext
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Address> Addresses { get; private set; }
    public DbSet<Order> Orderies { get; private set; }
    public DbSet<OrderItem> OrderItemes { get; private set; }
    public DbSet<Product> Products { get; private set; }

    public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
