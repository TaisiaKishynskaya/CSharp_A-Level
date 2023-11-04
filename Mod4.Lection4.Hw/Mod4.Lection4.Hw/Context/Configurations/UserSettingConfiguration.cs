using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw.Models;

namespace Mod4.Lection4.Hw.Context.Configurations;

public class UserSettingConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder
            .Property(x => x.Theme)
            .HasConversion(
                v => v.ToString(),
                v => (ThemeSetting)Enum.Parse(typeof(ThemeSetting), v)
            );
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.UserSettings)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
