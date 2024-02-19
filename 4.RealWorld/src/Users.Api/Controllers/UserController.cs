using Microsoft.AspNetCore.Mvc;

using Users.Api.Contracts;
using Users.Api.Mappers;
using Users.Api.Models;
using Users.Api.Services;

namespace Users.Api.Controllers;

[ ApiController ]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) => _userService = userService;

    [ HttpGet(template: "users") ]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<User>         users         = await _userService.GetAllAsync();
        IEnumerable<UserResponse> usersResponse = users.Select(x => x.ToUserResponse());

        return Ok(usersResponse);
    }

    [ HttpGet(template: "users/{id:guid}") ]
    public async Task<IActionResult> GetById(Guid id)
    {
        User? user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        UserResponse userResponse = user.ToUserResponse();

        return Ok(userResponse);
    }

    [ HttpPost(template: "users") ]
    public async Task<IActionResult> Create([ FromBody ] CreateUserRequest createUserRequest)
    {
        User user = new User { FullName = createUserRequest.FullName };

        bool created = await _userService.CreateAsync(user);

        if (!created)
            // Implement validation
            return BadRequest();

        UserResponse userResponse = user.ToUserResponse();

        return CreatedAtAction(nameof(GetById), new { id = userResponse.Id }, userResponse);
    }

    [ HttpDelete(template: "users/{id:guid}") ]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        bool deleted = await _userService.DeleteByIdAsync(id);

        if (!deleted)
            return NotFound();

        return Ok();
    }
}
