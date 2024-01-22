using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Catalog.API.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly ILogger<CatalogTypeService> _logger;

    public CatalogTypeService(
        ICatalogTypeRepository catalogTypeRepository,
        ILogger<CatalogTypeService> logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogTypeDTO>> Get(int pageIndex, int pageSize)
    {
        var types = await _catalogTypeRepository.Get();

        var dtoTypes = types
            .Select(type => new CatalogTypeDTO
            {
                Id = type.Id,
                Type = type.Type,
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new PaginatedItems<CatalogTypeDTO>(pageIndex, pageSize, dtoTypes.Count(), dtoTypes);

        return response;
    }

    public async Task<int> Add(CatalogTypeRequest catalogTypeRequest)
    {
        var existingCatalogType = await _catalogTypeRepository.GetByName(catalogTypeRequest.TypeName);
        if (existingCatalogType != null)
        {
            throw new ArgumentException($"Type with name {catalogTypeRequest.TypeName} already exists.");
        }

        var catalogType = new CatalogType { Type = catalogTypeRequest.TypeName };

        var id = await _catalogTypeRepository.Add(catalogType);

        return id;
    }

    public async Task<CatalogTypeDTO> Update(CatalogType catalogType)
    {
        var existingCatalogType = await _catalogTypeRepository.GetById(catalogType.Id);
        if (existingCatalogType == null)
        {
            throw new ArgumentException($"Type with id {catalogType.Id} does not exist.");
        }

        await _catalogTypeRepository.Update(catalogType);

        return new CatalogTypeDTO
        {
            Id = catalogType.Id,
            Type = catalogType.Type
        };
    }

    public async Task<int> Delete(int id)
    {
        var existingCatalogType = await _catalogTypeRepository.GetById(id);
        if (existingCatalogType == null)
        {
            throw new ArgumentException($"Type with id {id} does not exist.");
        }

        await _catalogTypeRepository.Delete(id);
        return id;
    }



}

