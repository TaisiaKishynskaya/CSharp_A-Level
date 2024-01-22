namespace Catalog.Data.Configurations;

public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogTypeEntity>
{
    public void Configure(EntityTypeBuilder<CatalogTypeEntity> builder)
    {
        builder.ToTable("CatalogType");

        builder.HasKey(type => type.Id);

        builder.Property(type => type.Title).IsRequired().HasMaxLength(50);
    }
}
