using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.DataAccess.Entities;

namespace Ordering.DataAccess.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Title).IsRequired().HasMaxLength(100);

        builder.Property(item => item.PictureUrl).IsRequired().HasMaxLength(50);

        builder.Property(item => item.Price).IsRequired();

        builder.Property(item => item.Quantity).IsRequired();

        builder.HasOne(item => item.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId);
    }
}
