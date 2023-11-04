using Microsoft.EntityFrameworkCore;

namespace Mod4.Lection4.Hw.Context;

public class EFCoreContext : DbContext
{
    // DbContext не має знати, де він буде використовуватись, тому тут не повинно бути конфігурації.
    // DbContext має описувати, що він вміє підключатись з якимись налаштуваннями

    public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options) { }
}
