using AutoMapper;
using IdentityModel;
using Ordering.Core.Abstractions.Repositories;
using Ordering.Core.Abstractions.Services;
using Ordering.DataAccess.Entities;
using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository<UserEntity> userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> Get(int page, int size)
    {
        var userEntities = await _userRepository.Get(page, size);

        var users = _mapper.Map<IEnumerable<User>>(userEntities);  

        return users;
    }

    public async Task<User> GetUserById(string userId)
    {
        var existedUserEntity = await _userRepository.GetUserById(userId);

        var user = _mapper.Map<User>(existedUserEntity);

        return user;
    }

    public async Task<User> Add(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = userClaims.FindFirst(JwtClaimTypes.Name)?.Value;
        var userEmail = userClaims.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        var userEntity = await _userRepository.GetUserById(userId);

        if (userEntity != null)
        {
            throw new Exception("User already exists");
        }

        userEntity = new UserEntity
        {
            UserId = userId,
            UserName = userName,
            UserEmail = userEmail
        };

        userEntity = await _userRepository.Add(userEntity);

        return _mapper.Map<User>(userEntity);
    }

    public string GetActiveUserId(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}
