

namespace Ordering.Domain.Models;

public record CatalogItem(
int Id,
string Title,
string Description,
decimal Price,
string PictureFile,
CatalogType Type,
CatalogBrand Brand,
int Quantity
);