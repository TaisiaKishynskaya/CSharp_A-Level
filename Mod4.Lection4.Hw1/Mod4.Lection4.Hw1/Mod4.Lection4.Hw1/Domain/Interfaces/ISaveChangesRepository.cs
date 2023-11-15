namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface ISaveChangesRepository
{
    Task<int> SaveChangesAsync();
}
