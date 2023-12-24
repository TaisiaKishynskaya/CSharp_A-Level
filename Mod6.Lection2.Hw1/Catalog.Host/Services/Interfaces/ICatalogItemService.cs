using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(CreateProductRequest catalogItem);
}
