using System;

namespace Catalog.API.Data;

public class CatalogDbContextSeed
{
    private readonly ILogger<CatalogDbContextSeed> _logger;

    public CatalogDbContextSeed(ILogger<CatalogDbContextSeed> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(CatalogDbContext context)
    {
        await context.Database.MigrateAsync();
        _logger.LogInformation("Database migrated");

        if (!context.CatalogBrands.Any())
        {
            await SeedCatalogBrands(context);
        }

        if (!context.CatalogTypes.Any())
        {
            await SeedCatalogTypes(context);
        }

        if (!context.CatalogItems.Any())
        {
            await SeedCatalogItems(context);
        }
    }

    private async Task SeedCatalogBrands(CatalogDbContext context)
    {
        context.CatalogBrands.AddRange(new List<CatalogBrand>()
    {
        new CatalogBrand() { Brand = "Brand1" },
        new CatalogBrand() { Brand = "Brand2" }
    });

        await context.SaveChangesAsync();
        _logger.LogInformation($"Seeded catalog with {context.CatalogBrands.Count()} brands");
    }

    private async Task SeedCatalogTypes(CatalogDbContext context)
    {
        context.CatalogTypes.AddRange(new List<CatalogType>()
    {
        new CatalogType() { Type = "Type1" },
        new CatalogType() { Type = "Type2" }
    });

        await context.SaveChangesAsync();
        _logger.LogInformation($"Seeded catalog with {context.CatalogTypes.Count()} types");
    }

    private async Task SeedCatalogItems(CatalogDbContext context)
    {
        context.CatalogItems.AddRange(new List<CatalogItem>()
    {
        new CatalogItem { Name = "Item 1", Description = "Description 1", Price = 100, PictureUri = "1.png", CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 10 },
        new CatalogItem { Name = "Item 2", Description = "Description 2", Price = 200, PictureUri = "2.png", CatalogTypeId = 2, CatalogBrandId = 1, AvailableStock = 15 },
        new CatalogItem { Name = "Item 3", Description = "Description 3", Price = 300, PictureUri = "3.png", CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 20 },
        new CatalogItem { Name = "Item 4", Description = "Description 4", Price = 400, PictureUri = "4.png", CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 25 },
        new CatalogItem { Name = "Item 5", Description = "Description 5", Price = 500, PictureUri = "5.png", CatalogTypeId = 2, CatalogBrandId = 1, AvailableStock = 5 }
    });

        await context.SaveChangesAsync();
        _logger.LogInformation($"Seeded catalog with {context.CatalogItems.Count()} items");
    }

}


