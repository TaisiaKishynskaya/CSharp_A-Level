using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Infrastructure.Context.Configurations;

public class OrderConfiguration
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Date).IsRequired();

        // *-1
        builder.HasOne(u => u.User).WithMany(o => o.Orders);

        // *-*
        builder.HasMany(oi => oi.OrderItems).WithMany(o => o.Orders);
    }
}
