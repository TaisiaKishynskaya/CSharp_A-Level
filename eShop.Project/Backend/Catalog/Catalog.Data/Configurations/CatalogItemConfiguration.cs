namespace Catalog.Data.Configurations;

public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItemEntity>
{
    public void Configure(EntityTypeBuilder<CatalogItemEntity> builder)
    {
        builder.ToTable("CatalogItem");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Title).IsRequired()
            .HasMaxLength(100);

        builder.Property(item => item.Description)
            .HasMaxLength(250);

        builder.Property(item => item.Price).IsRequired();

        builder.Property(item => item.PictureFile).IsRequired();

        builder.Property(item => item.TypeId).IsRequired();

        builder.Property(item => item.BrandId).IsRequired();

        builder.Property(item => item.CreatedAt).IsRequired();


        builder.HasOne(item => item.Type).WithMany().HasForeignKey(item => item.TypeId);
        builder.HasOne(item => item.Brand).WithMany().HasForeignKey(item => item.BrandId);
    }
}
