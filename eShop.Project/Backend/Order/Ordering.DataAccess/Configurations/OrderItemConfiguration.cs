using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.DataAccess.Entities;

namespace Ordering.DataAccess.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("OrderItemEntity");
        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.ItemId)
            .IsRequired();

        builder.Property(orderItem => orderItem.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(orderItem => orderItem.Price)
            .IsRequired();

        builder.Property(orderItem => orderItem.PictureUrl)
            .IsRequired()
            .HasMaxLength(75);

        builder.Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder.HasOne(orderItem => orderItem.OrderEntity).WithMany().HasForeignKey(orderItem => orderItem.OrderId);
    }

}
