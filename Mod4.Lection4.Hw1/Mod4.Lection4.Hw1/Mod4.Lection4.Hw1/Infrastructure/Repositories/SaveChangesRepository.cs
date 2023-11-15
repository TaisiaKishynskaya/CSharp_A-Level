using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Infrastructure.Context;

namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class SaveChangesRepository : ISaveChangesRepository
{
    private EFCoreContext _context;

    public SaveChangesRepository(EFCoreContext context) => _context = context;
    
    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
