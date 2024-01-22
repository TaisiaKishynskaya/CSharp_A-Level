using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
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

        public async Task<PaginatedItems<CatalogType>> GetByPageAsyncHttpGet(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogTypes.LongCountAsync();

            var types = await _dbContext.CatalogTypes
                .OrderBy(x => x.Id)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogType>
            {
                TotalCount = totalItems,
                Data = types
            };
        }

        public async Task<PaginatedItems<CatalogType>> GetTypesByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogTypes.LongCountAsync();

            var types = await _dbContext.CatalogTypes
                .OrderBy(x => x.Id)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogType>
            {
                TotalCount = totalItems,
                Data = types
            };
        }

        public async Task<CatalogType> GetTypeByIdAsync(int id)
        {
            return await _dbContext.CatalogTypes.FindAsync(id);
        }

        public async Task<int?> AddAsync(AddCatalogTypeRequest typeToAdd)
        {
            var type = await _dbContext.AddAsync(new CatalogType
            {
                Type = typeToAdd.Type
            });

            await _dbContext.SaveChangesAsync();

            return type.Entity.Id;
        }

        public async Task<CatalogType> UpdateAsync(UpdateCatalogTypeRequest typeToUpdate)
        {
            var type = await _dbContext.CatalogTypes.FindAsync(typeToUpdate.Id);
            if (type == null)
            {
                throw new KeyNotFoundException("Type not found");
            }

            type.Type = typeToUpdate.Type;

            await _dbContext.SaveChangesAsync();

            return type;
        }

        public async Task DeleteAsync(DeleteCatalogTypeRequest typeToDelete)
        {
            var type = await _dbContext.CatalogTypes.FindAsync(typeToDelete.Id);
            if (type == null)
            {
                throw new KeyNotFoundException("Type not found");
            }

            _dbContext.CatalogTypes.Remove(type);
            await _dbContext.SaveChangesAsync();
        }
    }
}