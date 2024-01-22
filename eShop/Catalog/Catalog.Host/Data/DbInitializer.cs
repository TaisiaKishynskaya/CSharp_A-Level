using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        await PreConfigure(context, context.CatalogBrands, GetPreconfiguredBrands);
        await PreConfigure(context, context.CatalogTypes, GetPreconfiguredTypes);
        await PreConfigure(context, context.CatalogItems, GetPreconfiguredItems);
    }

    private static async Task PreConfigure<T>(ApplicationDbContext context, DbSet<T> dbSet, Func<IEnumerable<T>> getPreconfiguredData) where T : class
    {
        if (!dbSet.Any())
        {
            await dbSet.AddRangeAsync(getPreconfiguredData());
            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogBrand> GetPreconfiguredBrands()
    {
        return new List<CatalogBrand>()
        {
            new CatalogBrand() { Brand = "Azure" },
            new CatalogBrand() { Brand = ".NET" },
            new CatalogBrand() { Brand = "Visual Studio" },
            new CatalogBrand() { Brand = "SQL Server" },
            new CatalogBrand() { Brand = "Other" }
        };
    }
    private static IEnumerable<CatalogType> GetPreconfiguredTypes()
    {
        return new List<CatalogType>()
        {
            new CatalogType() { Type = "Mug" },
            new CatalogType() { Type = "T-Shirt" },
            new CatalogType() { Type = "Sheet" },
            new CatalogType() { Type = "USB Memory Stick" }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Bot Black Hoodie", Name = ".NET Bot Black Hoodie", Price = 19.5M, PictureFileName = "1.png" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Black & White Mug", Name = ".NET Black & White Mug", Price = 8.50M, PictureFileName = "2.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Prism White T-Shirt", Name = "Prism White T-Shirt", Price = 12, PictureFileName = "3.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Foundation T-shirt", Name = ".NET Foundation T-shirt", Price = 12, PictureFileName = "4.png" },
            new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 5, AvailableStock = 100, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet", Price = 8.5M, PictureFileName = "5.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Blue Hoodie", Name = ".NET Blue Hoodie", Price = 12, PictureFileName = "6.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Roslyn Red T-Shirt", Name = "Roslyn Red T-Shirt", Price = 12, PictureFileName = "7.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Kudu Purple Hoodie", Name = "Kudu Purple Hoodie", Price = 8.5M, PictureFileName = "8.png" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 5, AvailableStock = 100, Description = "Cup<T> White Mug", Name = "Cup<T> White Mug", Price = 12, PictureFileName = "9.png" },
            new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 100, Description = ".NET Foundation Sheet", Name = ".NET Foundation Sheet", Price = 12, PictureFileName = "10.png" },
            new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 100, Description = "Cup<T> Sheet", Name = "Cup<T> Sheet", Price = 8.5M, PictureFileName = "11.png" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "Prism White TShirt", Name = "Prism White TShirt", Price = 12, PictureFileName = "12.png" },
        };
    }
}