using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mod4.Lection4.Hw1.Models;

namespace Mod4.Lection4.Hw1.Context.Configurations;

public class ProductConfiguration
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Name).IsRequired();

        // *-*
        builder.HasMany(x => x.ProductItems).WithMany(x => x.Products);
    }
}
