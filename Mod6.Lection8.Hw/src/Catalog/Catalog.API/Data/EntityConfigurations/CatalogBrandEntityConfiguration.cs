namespace Catalog.API.Data.EntityConfigurations;

public class CatalogBrandEntityConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable("CatalogBrand");

        builder.HasKey(brand => brand.Id);

        builder.Property(brand => brand.Id)
            .IsRequired();

        builder.Property(brand => brand.Brand)
            .HasMaxLength(50)
            .IsRequired();
    }
}

