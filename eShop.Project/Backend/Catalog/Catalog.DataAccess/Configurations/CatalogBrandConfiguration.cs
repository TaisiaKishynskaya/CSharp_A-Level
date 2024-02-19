namespace Catalog.DataAccess.Configurations;

public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrandEntity>
{
    public void Configure(EntityTypeBuilder<CatalogBrandEntity> builder)
    {
        builder.ToTable("CatalogBrand"); // указывает имя таблицы БД, в которой будет храниться сущность EntityTypeBuilder.

        builder.HasKey(brand => brand.Id); // указывает первичный ключ сущности, который будет представлен свойством Id.

        // настраивает св-во Title, указывая, что оно может содержать максимум 50 символов.
        builder.Property(brand => brand.Title).IsRequired().HasMaxLength(50);
    }
}