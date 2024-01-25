namespace Catalog.Data.Configurations;

public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrandEntity>
{
    public void Configure(EntityTypeBuilder<CatalogBrandEntity> builder)
    {
        builder.ToTable("CatalogBrand");

        builder.HasKey(brand => brand.Id);

        builder.Property(brand => brand.Title).IsRequired().HasMaxLength(50);
    }
}