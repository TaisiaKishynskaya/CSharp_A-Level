namespace Catalog.DataAccess.Configurations;

public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogTypeEntity>
{
    public void Configure(EntityTypeBuilder<CatalogTypeEntity> builder)
    {
        builder.ToTable("CatalogType"); // указывает имя таблицы БД, в которой будет храниться сущность EntityTypeBuilder.

        builder.HasKey(type => type.Id); // указывает первичный ключ сущности, который будет представлен свойством Id.

        // настраивает св-во Title, указывая, что оно может содержать максимум 50 символов.
        builder.Property(type => type.Title).IsRequired().HasMaxLength(50);
    }
}
