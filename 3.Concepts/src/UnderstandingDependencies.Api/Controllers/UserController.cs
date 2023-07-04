namespace UnderstandingDependencies.Api.Controllers;

[ ApiController ]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController() => _userService = new UserService(new UserRepository());

    [ HttpGet("users") ]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<User> users = await _userService.GetAllAsync();

        return Ok(users);
    }
}
