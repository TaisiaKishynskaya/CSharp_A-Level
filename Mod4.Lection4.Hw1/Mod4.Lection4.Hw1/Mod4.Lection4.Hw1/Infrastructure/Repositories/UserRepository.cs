using Microsoft.EntityFrameworkCore;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;
using Mod4.Lection4.Hw1.Infrastructure.Context;

namespace Mod4.Lection4.Hw1.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(EFCoreContext eFContext) : base(eFContext) { }

    public async Task CreateUserAsync(User user) => await CreateAsync(user);
    
    public async Task UpdateUserAsync(User user) => await UpdateAsync(user);

    public async Task DeleteUserAsync(User user) => await DeleteAsync(user);

    public async Task<IReadOnlyCollection<User>> GetAllUsersAsync() => await FindAllAsync().ToListAsync();

    public async Task<User?> GetUserAsync(Guid userId)
    {
        return await FindByConditionAsync(x => x.Id == userId).FirstOrDefaultAsync();
    }

    public async Task AddOrders(Guid id, ICollection<Order> orders)
    {
        var user = await GetUserAsync(id);
        if (user == null) return;
        user.Orders = orders;
        //await UpdateAsync(school);
    }
}


/*private readonly EFCoreContext _dbContext;

    public UserRepository(EFCoreContext dbContext) => _dbContext = dbContext;

    public async Task Insert(User user)
    {
        // Option 1: User model is a parameter
        // Option 2: User is being created here with the 'new' keyword

        await _dbContext.Users.AddAsync(user);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        // Option 1: retrieve, track, update property by property
        //var existingUser = await _dbContext.Users.FindAsync(user.Id);
        //if (existingUser == null) return;

        //existingUser.Name = user.Name;
        //existingUser.Surname = user.Surname;
        //await _dbContext.SaveChangesAsync();

        // Option 2: Full object
        _dbContext.Update(user);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        // Option 1: retrieve, track, update property by property
        //var existingUser = await _dbContext.Users.FindAsync(id);
        //if (existingUser == null) return;

        //_dbContext.Remove(existingUser);
        //await _dbContext.SaveChangesAsync();

        // Option 2: track as object, remove
        var tacker = new User { Id = id };
        _dbContext.Users.Attach(tacker);
        _dbContext.Remove(tacker);
        await _dbContext.SaveChangesAsync();
    }

    public async Task InsertOneToOne(int userId, Address address)
    {
        // Option 1: find, update
        var existingUser = await _dbContext.Users.FindAsync(userId);
        if (existingUser == null) return;

        existingUser.UserAddress = address;
        _ = await _dbContext.SaveChangesAsync();
    }*/
