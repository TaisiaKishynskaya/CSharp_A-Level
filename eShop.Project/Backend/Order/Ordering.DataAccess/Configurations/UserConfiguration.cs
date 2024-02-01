using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.DataAccess.Entities;

namespace Ordering.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.UserId);

        builder.Property(user => user.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(user => user.UserEmail)
            .IsRequired()
            .HasMaxLength(50);
    }
}
