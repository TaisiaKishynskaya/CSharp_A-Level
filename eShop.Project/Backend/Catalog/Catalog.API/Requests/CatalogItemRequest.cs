﻿namespace Catalog.API.Requests;

public class CatalogItemRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFile { get; set; }

    public CatalogTypeRequest Type { get; set; }
    public CatalogBrandRequest Brand { get; set; }

    public int Quantity { get; set; } = 1;

}
