namespace Catalog.Data.Infrastructure;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<CatalogTypeEntity> Types { get; set; }
    public DbSet<CatalogBrandEntity> Brands { get; set; }
    public DbSet<CatalogItemEntity> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CatalogTypeConfiguration());
        builder.ApplyConfiguration(new CatalogBrandConfiguration());
        builder.ApplyConfiguration(new CatalogItemConfiguration());

        CatalogDbSeed.Seed(builder);
    }
}
