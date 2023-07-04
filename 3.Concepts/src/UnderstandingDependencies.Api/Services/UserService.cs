namespace UnderstandingDependencies.Api.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _logger         = new Logger<UserService>(new LoggerFactory());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopWatch = Stopwatch.StartNew();

        try
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();

            return users;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("All users retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
        }
    }
}
