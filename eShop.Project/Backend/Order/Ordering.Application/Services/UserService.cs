namespace Ordering.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository<UserEntity> userRepository,
        IMapper mapper,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> Get(int page, int size)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var userEntities = await _userRepository.Get(page, size);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: Get, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            var users = _mapper.Map<IEnumerable<User>>(userEntities);
            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<User> GetUserById(string userId)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var existedUserEntity = await _userRepository.GetUserById(userId);
            var endTime = DateTime.UtcNow;

            if (existedUserEntity == null)
            {
                _logger.LogError($"Error: User with id = {userId} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"User with id = {userId} not found");
            }

            _logger.LogInformation($"Operation: GetUserById, UserId: {userId}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            var user = _mapper.Map<User>(existedUserEntity);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public string GetActiveUserId(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }

    public async Task<User> Add(ClaimsPrincipal userClaims)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var userId = userClaims.GetUserId();
            var userName = userClaims.GetUserName();
            var userEmail = userClaims.GetUserEmail();

            var userEntity = await _userRepository.GetUserById(userId);

            if (userEntity != null)
            {
                _logger.LogError($"Error: User already exists, Stack Trace: {Environment.StackTrace}");
                throw new Exception("User already exists");
            }

            userEntity = new UserEntity
            {
                UserId = userId,
                UserName = userName,
                UserEmail = userEmail
            };

            userEntity = await _userRepository.Add(userEntity);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: Add, UserId: {userId}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            return _mapper.Map<User>(userEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }
}