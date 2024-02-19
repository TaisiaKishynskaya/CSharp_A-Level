namespace Catalog.DataAccess.Infrastructure;

// Этот код представляет класс CatalogDbContext, который является контекстом базы данных для Entity Framework Core.

public class CatalogDbContext : DbContext
{
    // Это конструктор класса CatalogDbContext, который принимает параметр.
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    { // Базовому конструктору класса DbContext передается эти параметры для настройки контекста базы данных.
    }

    // Эти св-ва представляют наборы данных для соответствующих сущностей (CatalogTypeEntity, CatalogBrandEntity, CatalogItemEntity) в БД.
    // Они используются для выполнения операций CRUD (создание, чтение, обновление, удаление) с соответствующими сущностями в БД.
    public DbSet<CatalogTypeEntity> Types { get; set; }
    public DbSet<CatalogBrandEntity> Brands { get; set; }
    public DbSet<CatalogItemEntity> Items { get; set; }

    // Этот метод вызывается при создании модели БД и используется для настройки отображения сущностей на таблицы БД, а также для установки связей между сущностями.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Тут метод применяет конфигурации сущностей к объекту ModelBuilder с помощью ApplyConfiguration.
        //Конфигурации сущностей определяют схему бБД, включая таблицы, колонки, ограничения и связи.
        builder.ApplyConfiguration(new CatalogTypeConfiguration());
        builder.ApplyConfiguration(new CatalogBrandConfiguration());
        builder.ApplyConfiguration(new CatalogItemConfiguration());

        // Этот метод используется для заполнения БД начальными данными.
        // Он вызывает статический метод Seed из класса CatalogDbSeed, который принимает объект ModelBuilder и использует его для заполнения БД начальными данными.
        CatalogDbSeed.Seed(builder);
    }
}
