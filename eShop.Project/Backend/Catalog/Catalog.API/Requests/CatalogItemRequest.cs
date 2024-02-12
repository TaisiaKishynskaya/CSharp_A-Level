namespace Catalog.API.Requests;

public record CatalogItemRequest(
    string Title,
    string Description,
    decimal Price,
    string PictureFile,
    CatalogTypeRequest Type,
    CatalogBrandRequest Brand,
    int Quantity);
