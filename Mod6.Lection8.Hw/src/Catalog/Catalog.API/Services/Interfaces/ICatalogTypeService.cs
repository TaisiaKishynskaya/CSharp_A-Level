namespace Catalog.API.Services.Interfaces;

public interface ICatalogTypeService
{
    Task<PaginatedItems<CatalogTypeDTO>> Get(int pageIndex, int pageSize);
    Task<int> Add(CatalogTypeRequest catalogTypeRequest);
    Task<CatalogTypeDTO> Update(CatalogType catalogType);
    Task<int> Delete(int id);

}

