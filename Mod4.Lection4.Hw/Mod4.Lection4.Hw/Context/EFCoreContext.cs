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
        // Option 1
        //modelBuilder.ApplyConfiguration(new UserConfiguration());

        // Option 2 - працює через рефлексію, треба знайти тип проекту і взяли в нього асемблі, шукає всі реалізації IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}