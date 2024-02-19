namespace Catalog.DataAccess.Configurations;

// Этот код представляет конфигурацию сущности CatalogItemEntity для Entity Framework Core. 
// Он реализует интерфейс IEntityTypeConfiguration<CatalogItemEntity>, который требует реализации метода Configure, чтобы настроить сущность.

public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItemEntity>
{
    // Этот метод настраивает сущность CatalogItemEntity с использованием объекта EntityTypeBuilder, который предоставляет API для настройки сущностей EF Core.
    public void Configure(EntityTypeBuilder<CatalogItemEntity> builder)
    {
        // Настройки сущности CatalogItemEntity:
        builder.ToTable("CatalogItem"); // указывает имя таблицы БД, в которой будет храниться сущность CatalogItemEntity.

        builder.HasKey(item => item.Id); // указывает первичный ключ сущности, который будет представлен свойством Id.

        builder.Property(item => item.Title).IsRequired()
            .HasMaxLength(100); // настраивает св-во Title, указывая, что оно обязательное для заполнения и может содержать максимум 100 символов.

        builder.Property(item => item.Description)
            .HasMaxLength(250); // настраивает св-во Description, указывая, что оно может содержать максимум 250 символов.

        builder.Property(item => item.Price).IsRequired(); // настраивает св-во Price, указывая, что оно обязательное для заполнения.

        builder.Property(item => item.PictureFile).IsRequired(); // настраивает св-во PictureFile, указывая, что оно обязательное для заполнения.

        builder.Property(item => item.TypeId).IsRequired(); // настраивает св-во TypeId, указывая, что оно обязательное для заполнения.

        builder.Property(item => item.BrandId).IsRequired(); // настраивает св-во BrandId, указывая, что оно обязательное для заполнения.

        builder.Property(item => item.CreatedAt).IsRequired(); // настраивает св-во CreatedAt, указывая, что оно обязательное для заполнения.

        // Определение отношений:
        // указывает отношение "один ко многим" между сущностями CatalogItemEntity и CatalogTypeEntity с помощью свойства Type.
        builder.HasOne(item => item.Type).WithMany().HasForeignKey(item => item.TypeId);
        // указывает отношение "один ко многим" между сущностями CatalogItemEntity и CatalogBrandEntity с помощью свойства Brand.
        builder.HasOne(item => item.Brand).WithMany().HasForeignKey(item => item.BrandId);
    }
}
