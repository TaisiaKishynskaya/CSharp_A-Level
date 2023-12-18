using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogType>> GetTypesAsync();

        Task<int?> Add(CatalogType catalogType);

        Task<int?> Update(CatalogType catalogType);

        Task<bool?> Delete(CatalogType catalogType);
    }
}
