namespace Ordering.Core.Abstractions.Repositories;

public interface IUserRepository<UserEntity>
{
    Task<IEnumerable<UserEntity>> Get(int page, int size);
    Task<UserEntity> GetUserById(string userId);
    Task<UserEntity> Add(UserEntity userEntity);
}