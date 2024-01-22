namespace Catalog.API.Data.EntityConfigurations;

public class CatalogTypeEntityConfiguration : IEntityTypeConfiguration<Entities.CatalogType>
{
    public void Configure(EntityTypeBuilder<Entities.CatalogType> builder)
    {
        builder.ToTable("CatalogType");

        builder.HasKey(type => type.Id);

        builder.Property(type => type.Id)
            .IsRequired();

        builder.Property(type => type.Type)
            .HasMaxLength(50)
            .IsRequired();
    }
}
