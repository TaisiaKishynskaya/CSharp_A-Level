using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItem");

        builder.HasKey(catalogItem => catalogItem.Id);

        builder.Property(catalogItem => catalogItem.Id)
            .UseHiLo()
            .IsRequired();

        builder.Property(catalogItem => catalogItem.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(catalogItem => catalogItem.Price)
            .IsRequired();

        builder.Property(catalogItem => catalogItem.PictureFileName)
            .IsRequired(false);

        builder.Property(catalogItem => catalogItem.Description)
            .IsRequired(false);

        builder.HasOne(catalogType => catalogType.CatalogBrand)
            .WithMany()
            .HasForeignKey(catalogItem => catalogItem.CatalogTypeId);

        builder.HasOne(catalogBrand => catalogBrand.CatalogBrand)
            .WithMany()
            .HasForeignKey(catalogItem => catalogItem.CatalogBrandId);
    }
}