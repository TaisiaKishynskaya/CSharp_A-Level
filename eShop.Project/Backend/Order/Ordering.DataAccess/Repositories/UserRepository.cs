using Microsoft.EntityFrameworkCore;
using Ordering.Core.Abstractions.Repositories;
using Ordering.DataAccess.Entities;
using Ordering.DataAccess.Infrastructure;

namespace Ordering.DataAccess.Repositories;

public class UserRepository : IUserRepository<UserEntity>
{
    private readonly OrderDbContext _dbContext;

    public UserRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserEntity>> Get(int page, int size)
    {
        IQueryable<UserEntity> query = _dbContext.Users
            .AsNoTracking()
            .OrderBy(user => user.UserId);

        if (page > 0 && size > 0)
        {
            query = query
                .Skip((page - 1) * size)
                .Take(size);
        }

        return await query.ToListAsync();
    }

    public async Task<UserEntity> GetUserById(string userId)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(userEntity => userEntity.UserId == userId);
        return userEntity;
    }

    public async Task<UserEntity> Add(UserEntity userEntity)
    {
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
        return userEntity;
    }
}
