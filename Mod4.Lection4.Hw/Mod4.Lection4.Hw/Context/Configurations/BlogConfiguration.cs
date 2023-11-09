using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mod4.Lection4.Hw.Models;

namespace Mod4.Lection4.Hw.Context.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasMany(x => x.Articles).WithOne(x => x.Blog);
        builder.HasMany(x => x.Readers).WithMany(x => x.BlogSubscribsions);
    }
}
