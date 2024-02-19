using Microsoft.AspNetCore.Mvc;

using Users.Api.Contracts;
using Users.Api.Mappers;

namespace Users.Api.Tests.Unit;

public sealed class UserControllerTests
{
    private readonly UserController _sut;

    private readonly IUserService _userService = Substitute.For<IUserService>();

    public UserControllerTests() => _sut = new UserController(_userService);

    [ Fact ]
    public async Task GetById_ShouldReturnOkAndObject_WhenUserExists()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), FullName = "John Doe" };

        _userService.GetByIdAsync(user.Id)
                    .Returns(user);

        // Act
        IActionResult result = await _sut.GetById(user.Id);

        // Assert
        result.Should()
              .BeOfType<OkObjectResult>()
              .Which.StatusCode.Should()
              .Be(expected: 200);

        result.Should()
              .BeOfType<OkObjectResult>()
              .Which.Value.Should()
              .BeAssignableTo<UserResponse>()
              .And.BeEquivalentTo(user.ToUserResponse());
    }

    [ Fact ]
    public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userService.GetByIdAsync(Arg.Any<Guid>())
                    .ReturnsNull();

        // Act
        IActionResult result = await _sut.GetById(Guid.NewGuid());

        // Assert
        result.Should()
              .BeOfType<NotFoundResult>()
              .Which.StatusCode.Should()
              .Be(expected: 404);
    }

    [ Fact ]
    public async Task GetAll_ShouldReturnEmptyEnumerable_WhenNoUsersExist()
    {
        // Arrange
        _userService.GetAllAsync()
                    .Returns(Enumerable.Empty<User>());

        // Act
        IActionResult result = await _sut.GetAll();

        // Assert
        result.Should()
              .BeOfType<OkObjectResult>()
              .Which.StatusCode.Should()
              .Be(expected: 200);

        result.Should()
              .BeOfType<OkObjectResult>()
              .Which.Value.Should()
              .BeAssignableTo<IEnumerable<UserResponse>>()
              .And.Subject.As<IEnumerable<UserResponse>>()
              .Should()
              .BeEmpty();
    }

    [ Fact ]
    public async Task GetAll_ShouldReturnUserResponse_WhenUsersExist()
    {
        // Arrange
        IEnumerable<User> users = new[]
                                  {
                                      new User { Id = Guid.NewGuid(), FullName = "John Doe" }, new User { Id = Guid.NewGuid(), FullName = "Jane Doe" }
                                  };

        _userService.GetAllAsync()
                    .Returns(users);

        // Act
        IActionResult result = await _sut.GetAll();

        // Assert
        result.Should()
              .BeOfType<OkObjectResult>()
              .Which.StatusCode.Should()
              .Be(expected: 200);

        result.Should()
              .BeOfType<OkObjectResult>()
              .Which.Value.Should()
              .BeAssignableTo<IEnumerable<UserResponse>>()
              .And.Subject.As<IEnumerable<UserResponse>>()
              .Should()
              .BeEquivalentTo(users.Select(x => x.ToUserResponse()));
    }
}
