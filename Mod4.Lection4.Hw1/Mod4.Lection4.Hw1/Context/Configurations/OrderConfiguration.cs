using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mod4.Lection4.Hw1.Models;

namespace Mod4.Lection4.Hw1.Context.Configurations;

public class OrderConfiguration
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Date).IsRequired();

        // *-1
        builder.HasOne(x => x.User).WithMany(x => x.Orders);

        // *-*
        builder.HasMany(x => x.OrderItems).WithMany(x => x.Orders);
    }
}
