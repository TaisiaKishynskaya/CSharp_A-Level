﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Infrastructure.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Username).IsRequired();

        // 1-1
        builder
            .HasOne(x => x.UserAddress)
            .WithOne(x => x.User)
            .HasForeignKey<Address>(c => c.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        // 1-*
        builder.HasMany(x => x.Orders).WithOne(x => x.User);
    }
}
