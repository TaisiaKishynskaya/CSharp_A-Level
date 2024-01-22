namespace Catalog.API.Models;

public class PaginatedItemsRequest
{
    [DefaultValue(1)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public int PageIndex { get; set; }

    [DefaultValue(5)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public int PageSize { get; set; }

    [DefaultValue(new string[] { "Type1", })]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public List<string> Types { get; set; }

    [DefaultValue(new string[] { "Brand1",})]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public List<string> Brands { get; set; }
}