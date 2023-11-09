using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mod4.Lection4.Hw.Models;

namespace Mod4.Lection4.Hw.Context.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasOne(x => x.Blog).WithMany(x => x.Articles);
        builder.HasMany(x => x.Athors).WithMany(x => x.Articles);
    }
}
