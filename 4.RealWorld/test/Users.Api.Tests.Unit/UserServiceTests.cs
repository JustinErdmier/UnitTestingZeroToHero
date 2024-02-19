using NSubstitute.ReturnsExtensions;

namespace Users.Api.Tests.Unit;

public sealed class UserServiceTests
{
    private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();

    private readonly UserService _sut;

    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    public UserServiceTests() => _sut = new UserService(_userRepository, _logger);

    [ Fact ]
    public async Task GetAllAsync_ShouldReturnEmptyCollection_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync()
                       .Returns(Enumerable.Empty<User>());

        // Act
        IEnumerable<User> result = await _sut.GetAllAsync();

        // Assert
        result.Should()
              .BeEmpty();
    }

    [ Fact ]
    public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExist()
    {
        // Arrange
        IEnumerable<User> expectedUsers = new[]
                                          {
                                              new User { Id = Guid.NewGuid(), FullName = "John Doe" },
                                              new User { Id = Guid.NewGuid(), FullName = "Jane Doe" }
                                          };

        _userRepository.GetAllAsync()
                       .Returns(expectedUsers);

        // Act
        IEnumerable<User> result = await _sut.GetAllAsync();

        // Assert
        result.Should()
              .BeEquivalentTo(expectedUsers);
    }

    [ Fact ]
    public async Task GetAllAsync_ShouldLogInformation_WhenInvoked()
    {
        // Arrange
        _userRepository.GetAllAsync()
                       .Returns(Enumerable.Empty<User>());

        // Act
        await _sut.GetAllAsync();

        // Assert
        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "Retrieving all users"));

        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "All users retrieved in {0}ms"), Arg.Any<long>());
    }

    [ Fact ]
    public async Task GetAllAsync_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        _userRepository.GetAllAsync()
                       .Throws(new SqliteException(message: "Something went wrong", errorCode: 500));

        // Act
        Func<Task> action = async () => await _sut.GetAllAsync();

        // Assert
        await action.Should()
                    .ThrowAsync<SqliteException>()
                    .WithMessage(expectedWildcardPattern: "Something went wrong");

        _logger.Received(requiredNumberOfCalls: 1)
               .LogError(Arg.Any<SqliteException>(), Arg.Is(value: "Something went wrong while retrieving all users"));
    }

    [ Fact ]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        User expectedUser = new() { Id = Guid.NewGuid(), FullName = "John Doe" };

        _userRepository.GetByIdAsync(expectedUser.Id)
                       .Returns(expectedUser);

        // Act
        User? result = await _sut.GetByIdAsync(expectedUser.Id);

        // Assert
        result.Should()
              .NotBeNull()
              .And.BeEquivalentTo(expectedUser);
    }

    [ Fact ]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepository.GetByIdAsync(Arg.Any<Guid>())
                       .ReturnsNull();

        // Act
        User? result = await _sut.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should()
              .BeNull();
    }

    [ Fact ]
    public async Task GetByIdAsync_ShouldLogInformation_WhenInvoked()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        _userRepository.GetByIdAsync(userId)
                       .ReturnsNull();

        // Act
        await _sut.GetByIdAsync(userId);

        // Assert
        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "Retrieving user with id: {0}"), Arg.Is(userId));

        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "User with id {0} retrieved in {1}ms"), Arg.Is(userId), Arg.Any<long>());
    }

    [ Fact ]
    public async Task GetByIdAsync_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        Guid            userId          = Guid.NewGuid();
        SqliteException sqliteException = new(message: "Something went wrong", errorCode: 500);

        _userRepository.GetByIdAsync(userId)
                       .Throws(sqliteException);

        // Act
        Func<Task> action = async () => await _sut.GetByIdAsync(userId);

        // Assert
        await action.Should()
                    .ThrowAsync<SqliteException>()
                    .WithMessage(sqliteException.Message);

        _logger.Received(requiredNumberOfCalls: 1)
               .LogError(Arg.Is(sqliteException), Arg.Is(value: "Something went wrong while retrieving user with id {0}"), Arg.Is(userId));
    }

    [ Fact ]
    public async Task CreateAsync_ShouldReturnTrue_WhenUserIsCreated()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), FullName = "John Doe" };

        _userRepository.CreateAsync(user)
                       .Returns(returnThis: true);

        // Act
        bool result = await _sut.CreateAsync(user);

        // Assert
        result.Should()
              .BeTrue();
    }

    [ Fact ]
    public async Task CreateAsync_ShouldReturnFalse_WhenUserIsNotCreated()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), FullName = "John Doe" };

        _userRepository.CreateAsync(user)
                       .Returns(returnThis: false);

        // Act
        bool result = await _sut.CreateAsync(user);

        // Assert
        result.Should()
              .BeFalse();
    }

    [ Fact ]
    public async Task CreateAsync_ShouldLogInformation_WhenInvoked()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), FullName = "John Doe" };

        _userRepository.CreateAsync(user)
                       .Returns(returnThis: true);

        // Act
        await _sut.CreateAsync(user);

        // Assert
        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "Creating user with id {0} and name: {1}"), Arg.Is(user.Id), Arg.Is(user.FullName));

        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "User with id {0} created in {1}ms"), Arg.Is(user.Id), Arg.Any<long>());
    }

    [ Fact ]
    public async Task CreateAsync_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        User            user            = new() { Id = Guid.NewGuid(), FullName = "John Doe" };
        SqliteException sqliteException = new(message: "Something went wrong", errorCode: 500);

        _userRepository.CreateAsync(Arg.Any<User>())
                       .Throws(sqliteException);

        // Act
        Func<Task> action = async () => await _sut.CreateAsync(user);

        // Assert
        await action.Should()
                    .ThrowAsync<SqliteException>()
                    .WithMessage(sqliteException.Message);

        _logger.Received(requiredNumberOfCalls: 1)
               .LogError(Arg.Is(sqliteException), Arg.Is(value: "Something went wrong while creating a user"));
    }

    [ Fact ]
    public async Task DeleteByIdAsync_ShouldReturnTrue_WhenUserIsDeleted()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        _userRepository.DeleteByIdAsync(userId)
                       .Returns(returnThis: true);

        // Act
        bool result = await _sut.DeleteByIdAsync(userId);

        // Assert
        result.Should()
              .BeTrue();
    }

    [ Fact ]
    public async Task DeleteByIdAsync_ShouldReturnFalse_WhenUserIsNotDeleted()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        _userRepository.DeleteByIdAsync(userId)
                       .Returns(returnThis: false);

        // Act
        bool result = await _sut.DeleteByIdAsync(userId);

        // Assert
        result.Should()
              .BeFalse();
    }

    [ Fact ]
    public async Task DeleteByIdAsync_ShouldLogInformation_WhenInvoked()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        _userRepository.DeleteByIdAsync(userId)
                       .Returns(returnThis: true);

        // Act
        await _sut.DeleteByIdAsync(userId);

        // Assert
        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "Deleting user with id: {0}"), Arg.Is(userId));

        _logger.Received(requiredNumberOfCalls: 1)
               .LogInformation(Arg.Is(value: "User with id {0} deleted in {1}ms"), Arg.Is(userId), Arg.Any<long>());
    }

    [ Fact ]
    public async Task DeleteByIdAsync_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        Guid            userId          = Guid.NewGuid();
        SqliteException sqliteException = new(message: "Something went wrong", errorCode: 500);

        _userRepository.DeleteByIdAsync(userId)
                       .Throws(sqliteException);

        // Act
        Func<Task> action = async () => await _sut.DeleteByIdAsync(userId);

        // Assert
        await action.Should()
                    .ThrowAsync<SqliteException>()
                    .WithMessage(sqliteException.Message);

        _logger.Received(requiredNumberOfCalls: 1)
               .LogError(Arg.Is(sqliteException), Arg.Is(value: "Something went wrong while deleting user with id {0}"), Arg.Is(userId));
    }
}
