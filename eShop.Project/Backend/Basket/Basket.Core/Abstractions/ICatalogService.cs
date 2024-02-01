using System.Threading.Tasks;
using Basket.Domain.Models;

namespace Basket.Core.Abstractions;

public interface ICatalogService
{
    Task<CatalogItem> GetItemById(int id);
}