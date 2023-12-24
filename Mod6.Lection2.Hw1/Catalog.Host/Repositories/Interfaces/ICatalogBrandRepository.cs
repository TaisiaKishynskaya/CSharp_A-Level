using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();

        Task<int?> Add(CatalogBrand catalogBrand);

        Task<bool?> Delete(CatalogBrand catalogBrand);

        Task<int?> Update(CatalogBrand catalogBrand);
    }
}
