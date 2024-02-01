using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.DataAccess.Entities;

namespace Ordering.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(order =>  order.Id);

        builder.Property(order => order.Address)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(order => order.User)
            .WithMany()
            .HasForeignKey(order => order.UserId);

        builder.HasMany(order => order.OrderItems)
            .WithOne(orderEntity => orderEntity.OrderEntity)
            .HasForeignKey(order => order.Id);
    }
}
