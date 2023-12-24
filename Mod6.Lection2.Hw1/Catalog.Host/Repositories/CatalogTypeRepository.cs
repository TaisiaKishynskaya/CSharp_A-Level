using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var types = _dbContext.CatalogTypes.ToListAsync();

            return Task.FromResult(types.Result.AsEnumerable());
        }

        public async Task<int?> Add(CatalogType catalogType)
        {
            var result = await _dbContext.CatalogTypes.AddAsync(catalogType);

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<int?> Update(CatalogType catalogType)
        {
            var result = _dbContext.CatalogTypes.Update(catalogType);

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<bool?> Delete(CatalogType catalogType)
        {
            var result = _dbContext.CatalogTypes.Remove(catalogType);

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id > 0;
        }
    }
}