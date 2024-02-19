# `UserServiceTests`

- [x] `GetByIdAsync` should return a user when a user exists
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
      {
          // Arrange
          var existingUser = new User
          {
              Id = Guid.NewGuid(),
              FullName = "Nick Chapsas"
          };
          _userRepository.GetByIdAsync(existingUser.Id).Returns(existingUser);

          // Act
          var result = await _sut.GetByIdAsync(existingUser.Id);

          // Assert
          result.Should().BeEquivalentTo(existingUser);
      }
      ```
      </details>
- [x] `GetByIdAsync` should return null, when a user doesn't exist
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task GetByIdAsync_ShouldReturnNull_WhenNoUserExists()
      {   
          // Arrange
          _userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();
      
          // Act
          var result = await _sut.GetByIdAsync(Guid.NewGuid());

          // Assert
          result.Should().BeNull();
      }
      ```
      </details>
- [x] `GetByIdAsync` should log the correct messages when retrieving the users
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task GetByIdAsync_ShouldLogMessages_WhenInvoked()
      {
          // Arrange
          var userId = Guid.NewGuid();
          _userRepository.GetByIdAsync(userId).ReturnsNull();

          // Act
          await _sut.GetByIdAsync(userId);

          // Assert
          _logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}"),
              Arg.Is(userId));
          _logger.Received(1).LogInformation(Arg.Is("User with id {0} retrieved in {1}ms"),
              Arg.Is(userId), Arg.Any<long>());
      }
      ```
      </details>
- [x] `GetByIdAsync` should log the correct messages when an exception is thrown
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task GetByIdAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
      {
          // Arrange
          var userId = Guid.NewGuid();
          var sqliteException = new SqliteException("Something went wrong", 500);
          _userRepository.GetByIdAsync(userId)
              .Throws(sqliteException);

          // Act
          var requestAction = async () => await _sut.GetByIdAsync(userId);

          // Assert
          await requestAction.Should()
              .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
          _logger.Received(1).LogError(Arg.Is(sqliteException),
              Arg.Is("Something went wrong while retrieving user with id {0}"),
              Arg.Is(userId));
      }
      ```
      </details>
- [x] `CreateAsync` should create a user when user create details are valid
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValid()
      {
          // Arrange
          var user = new User
          {
              Id = Guid.NewGuid(),
              FullName = "Nick Chapsas"
          };
          _userRepository.CreateAsync(user).Returns(true);

          // Act
          var result = await _sut.CreateAsync(user);

          // Assert
          result.Should().BeTrue();
      }
      ```
      </details>
- [x] `CreateAsync` should log the correct messages when creating a user
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task CreateAsync_ShouldLogMessages_WhenInvoked()
      {
          // Arrange
          var user = new User
          {
              Id = Guid.NewGuid(),
              FullName = "Nick Chapsas"
          };
          _userRepository.CreateAsync(user).Returns(true);

          // Act
          await _sut.CreateAsync(user);

          // Assert
          _logger.Received(1).LogInformation(Arg.Is("Creating user with id {0} and name: {1}"),
              Arg.Is(user.Id), Arg.Is(user.FullName));
          _logger.Received(1).LogInformation(Arg.Is("User with id {0} created in {1}ms"),
              Arg.Is(user.Id), Arg.Any<long>());
      }
      ```
      </details>
- [x] `CreateAsync` should log the correct messages when an exception is thrown
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task CreateAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
      {
          // Arrange
          var user = new User
          {
              Id = Guid.NewGuid(),
              FullName = "Nick Chapsas"
          };
          var sqliteException = new SqliteException("Something went wrong", 500);
          _userRepository.CreateAsync(user)
              .Throws(sqliteException);
      
          // Act
          var requestAction = async () => await _sut.CreateAsync(user);
      
          // Assert
          await requestAction.Should()
              .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
          _logger.Received(1).LogError(Arg.Is(sqliteException),
              Arg.Is("Something went wrong while creating a user"));
      }
      ```
      </details>
- [x] `DeleteByIdAsync` should delete a user when the user exists
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExists()
      {
          // Arrange
          var userId = Guid.NewGuid();
          _userRepository.DeleteByIdAsync(userId).Returns(true);
          
          // Act
          var result = await _sut.DeleteByIdAsync(userId);
          
          // Assert
          result.Should().BeTrue();
      }
      ```
      </details>
- [x] `DeleteByIdAsync` should not delete a user does not exist
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task DeleteByIdAsync_ShouldNotDeleteUser_WhenUserDoesntExists()
      {
          // Arrange
          var userId = Guid.NewGuid();
          _userRepository.DeleteByIdAsync(userId).Returns(false);

          // Act
          var result = await _sut.DeleteByIdAsync(userId);

          // Assert
          result.Should().BeFalse();
      }
      ```
      </details>
- [x] `DeleteByIdAsync` should log the correct messages when deleting a user
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task DeleteByIdAsync_ShouldLogMessages_WhenInvoked()
      {
          // Arrange
          var userId = Guid.NewGuid();
          _userRepository.DeleteByIdAsync(userId).Returns(true);
      
          // Act
          await _sut.DeleteByIdAsync(userId);
      
          // Assert
          _logger.Received(1).LogInformation(Arg.Is("Deleting user with id: {0}"),
              Arg.Is(userId));
          _logger.Received(1).LogInformation(Arg.Is("User with id {0} deleted in {1}ms"),
              Arg.Is(userId), Arg.Any<long>());
      }
      ```
      </details>
- [x] `DeleteByIdAsync` should log the correct messages when an exception is thrown
    - <details>
      <summary>Solution</summary>

      ```csharp
      [Fact]
      public async Task DeleteByIdAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
      {
          // Arrange
          var userId = Guid.NewGuid();
          var sqliteException = new SqliteException("Something went wrong", 500);
          _userRepository.DeleteByIdAsync(userId)
              .Throws(sqliteException);
      
          // Act
          var requestAction = async () => await _sut.DeleteByIdAsync(userId);
      
          // Assert
          await requestAction.Should()
              .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
          _logger.Received(1).LogError(Arg.Is(sqliteException),
              Arg.Is("Something went wrong while deleting user with id {0}"),
              Arg.Is(userId));
      }
      ```
      </details>

# `UserControllerTests`

- [ ] `Create` Should return the user with a 201 status code when user was created
    - <details>
      <summary>Solution</summary>

      ```csharp
      TBA
      ```
      </details>
- [ ] `Create` Should return 400 status code when the user wasn't created
    - <details>
      <summary>Solution</summary>

      ```csharp
      TBA
      ```
      </details>
- [ ] `DeleteById` Should return 200 when the user was deleted successfully
    - <details>
      <summary>Solution</summary>

      ```csharp
      TBA
      ```
      </details>
- [ ] `DeleteById` Should return 404 when the user was not deleted
    - <details>
      <summary>Solution</summary>

      ```csharp
      TBA
      ```
      </details>
