﻿namespace Catalog.API.Models;

public class CatalogItemRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUri { get; set; }
    public int TypeId { get; set; }
    public int BrandId { get; set; }
    public int AvailableStock { get; set; }
}
