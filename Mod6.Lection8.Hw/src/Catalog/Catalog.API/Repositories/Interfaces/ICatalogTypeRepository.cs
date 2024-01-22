namespace Catalog.API.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> Get();
    Task<int> Add(CatalogType catalogType);
    Task Update(CatalogType catalogType);
    Task Delete(int id);

    Task<CatalogType> GetByName(string typeName);
    Task<CatalogType> GetById(int id);
}

