using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mod4.Lection4.Hw.Models;
using System.Reflection.Metadata;

namespace Mod4.Lection4.Hw.Context;

public class EFCoreContext : DbContext
{
    // DbContext не має знати, де він буде використовуватись, тому тут не повинно бути конфігурації.
    // DbContext має описувати, що він вміє підключатись з якимись налаштуваннями

    // Fluent API

    public DbSet<User> Users { get; private set; }  // щоб EF почав працювати з моделью з коду до бд

    public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);  // вказуємо, що є ключем
        modelBuilder.Entity<User>().Property(b => b.Name).IsRequired();  // не null
        modelBuilder.Entity<User>().Property(b => b.Surname).IsRequired();

        // конвертація типа, кастомний конвертор
        modelBuilder.Entity<User>().Property(x => x.Birthday)
            .HasConversion(new ValueConverter<DateOnly?, DateTime?>(
                x => x.HasValue ? x.Value.ToDateTime(TimeOnly.MinValue) : null,
                y => y.HasValue ? DateOnly.FromDateTime(y.Value) : null)
            );

        
        modelBuilder.Entity<User>()
            .HasOne(x => x.UserSettings) // в юзера є один UserSettings
            .WithOne(x => x.User)  // де є 1 користувач
            .OnDelete(DeleteBehavior.ClientSetNull); // поведінка дляя форінг ключей (тільки на апдейт або видалення) - коли видаляєв налаштування, користувач не видалиться

        modelBuilder.Entity<User>().HasMany(x => x.Articles).WithMany(x => x.Athors);
        modelBuilder.Entity<User>().HasMany(x => x.BlogSubscribsions).WithMany(x => x.Readers);

        // ------------- User settings
        modelBuilder.Entity<UserSettings>().Property(x => x.Theme)
            .HasConversion(
                v => v.ToString(),
                v => (ThemeSetting)Enum.Parse(typeof(ThemeSetting), v)
            );
        modelBuilder.Entity<UserSettings>()
            .HasOne(x => x.User)
            .WithOne(x => x.UserSettings)
            .OnDelete(DeleteBehavior.Cascade);

        // ------------- Blog
        modelBuilder.Entity<Blog>().HasMany(x => x.Articles).WithOne(x => x.Blog); // багато статей мають 1 блог
        modelBuilder.Entity<Blog>().HasMany(x => x.Readers).WithMany(x => x.BlogSubscribsions); // багато до багатьох

        // ------------- Article
        modelBuilder.Entity<Article>().HasOne(x => x.Blog).WithMany(x => x.Articles);  // 1 блог має багато статей
        modelBuilder.Entity<Article>().HasMany(x => x.Athors).WithMany(x => x.Articles);
    }
}