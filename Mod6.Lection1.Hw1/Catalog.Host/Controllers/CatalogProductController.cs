using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogProductController : ControllerBase
{
    private static readonly string[] ProductsSummaries = 
    {
        "Tomato", "Banana", "Chilly", "Onion", "Butter", "Water", "Lemon", "Tea", "Cake", "Pie"
    };

    private readonly ILogger<CatalogProductController> _logger;

    public CatalogProductController(ILogger<CatalogProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Product> Get()
    {
        var products =  Enumerable.Range(1, 5).Select(index => new Product
            {
                Date = DateTime.Now.AddDays(index),
                PriceWithDiscont = Random.Shared.Next(0, 50),
                Summary = ProductsSummaries[Random.Shared.Next(ProductsSummaries.Length)]
            })
            .ToArray();

        _logger.LogInformation("Return {count} products.", ProductsSummaries.Length);

        return products;
    }
}