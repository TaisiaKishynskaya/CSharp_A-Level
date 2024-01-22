namespace Catalog.API.Data.EntityConfigurations;

public class CatalogItemEntityConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItem");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Id)
            .IsRequired();

        builder.Property(item => item.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(item => item.Description)
            .HasMaxLength(150);

        builder.Property(item => item.Price)
            .IsRequired();

        builder.HasIndex(item => item.Name);

        builder.HasOne(type => type.CatalogType).WithMany();

        builder.HasOne(brand => brand.CatalogBrand).WithMany();
    }
}

