namespace Catalog.API.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {       
    }

    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CatalogTypeEntityConfiguration());
        builder.ApplyConfiguration(new CatalogBrandEntityConfiguration());
        builder.ApplyConfiguration(new CatalogItemEntityConfiguration());
    }

}

