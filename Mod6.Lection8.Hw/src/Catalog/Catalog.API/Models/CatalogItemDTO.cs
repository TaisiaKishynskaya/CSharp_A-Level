namespace Catalog.API.Models
{
    public class CatalogItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public int AvailableStock { get; set; }
    }
}
