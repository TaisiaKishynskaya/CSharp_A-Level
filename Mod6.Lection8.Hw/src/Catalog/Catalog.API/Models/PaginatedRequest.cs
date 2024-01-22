namespace Catalog.API.Models;

public class PaginatedRequest
{
    [DefaultValue(1)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public int PageIndex { get; set; }

    [DefaultValue(5)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
    public int PageSize { get; set; }
}

