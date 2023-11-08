using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Infrastructure.Context.Configurations;

public class OrderItemConfiguration
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Quantity).IsRequired();

        // *-*
        builder.HasMany(x => x.Orders).WithMany(x => x.OrderItems);

        // *-*
        builder.HasMany(x => x.Products).WithMany(x => x.ProductItems);
    }
}
