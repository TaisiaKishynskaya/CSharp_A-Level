using Mod4.Lection4.Hw1.Context;

namespace Mod4.Lection4.Hw1.Repository;

public class UserRepository
{
    private readonly EFCoreContext _dbContext;

    public UserRepository(EFCoreContext dbContext)
    {
        _dbContext = dbContext;
    }
}
