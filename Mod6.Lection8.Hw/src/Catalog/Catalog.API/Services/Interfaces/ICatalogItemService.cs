namespace Catalog.API.Services.Interfaces;

public interface ICatalogItemService
{
    Task<PaginatedItems<CatalogItemDTO>> Get(int pageIndex, int pageSize);
    Task<CatalogItemDTO> GetById(int id);
    Task<string> GetPicturePathById(int id);
    Task<string> GetPictureUriById(int id);
    Task<int> Add(CatalogItemRequest catalogItemRequest);
    Task<CatalogItemDTO> Update(CatalogItem catalogItem);
    Task<int> Delete(int id);
}

