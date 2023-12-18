using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            var brands = await _dbContext.CatalogBrands.ToListAsync();

            return brands;
        }

        public async Task<int?> Add(CatalogBrand catalogBrand)
        {
            var brand = await _dbContext.AddAsync(catalogBrand);

            await _dbContext.SaveChangesAsync();

            return brand.Entity.Id;
        }

        public async Task<bool?> Delete(CatalogBrand catalogBrand)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(b => b.Id == catalogBrand.Id);

            if (brand == null)
            {
                return false;
            }

            _dbContext.CatalogBrands.Remove(brand);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<int?> Update(CatalogBrand catalogBrand)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(b => b.Id == catalogBrand.Id);

            if (brand == null)
            {
                return null;
            }

            _dbContext.CatalogBrands.Update(catalogBrand);

            await _dbContext.SaveChangesAsync();

            return brand.Id;
        }
    }
}