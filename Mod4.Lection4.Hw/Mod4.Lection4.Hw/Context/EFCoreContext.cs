using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mod4.Lection4.Hw.Models;

namespace Mod4.Lection4.Hw.Context;

public class EFCoreContext : DbContext
{
    // DbContext не має знати, де він буде використовуватись, тому тут не повинно бути конфігурації.
    // DbContext має описувати, що він вміє підключатись з якимись налаштуваннями

    // Fkuent API

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
    }
}
