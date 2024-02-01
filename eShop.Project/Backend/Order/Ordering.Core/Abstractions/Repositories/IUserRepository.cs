namespace Ordering.Core.Abstractions.Repositories;

public interface IUserRepository<UserEntity>
{
    Task<IEnumerable<UserEntity>> Get(int page, int size);
    Task<UserEntity> Add(UserEntity userEntity);
    Task<UserEntity> GetUserById(string userId);
}