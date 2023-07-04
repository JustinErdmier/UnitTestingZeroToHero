﻿using FluentAssertions;

using NSubstitute;

using UnderstandingDependencies.Api.Services;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    #region MyCode



    #endregion

    #region CourseSourceCode

    // private readonly UserService _sut;
    //
    // private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    // //private readonly Mock<IUserRepository> _userRepositoryMock = new();
    //
    // public UserServiceTests()
    // {
    //     _sut = new UserService(_userRepository);
    // }
    //
    // [ Fact ]
    // public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    // {
    //     // Arrange
    //     _userRepository.GetAllAsync().Returns(Array.Empty<User>());
    //     //_userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Array.Empty<User>());
    //
    //     // Act
    //     var users = await _sut.GetAllAsync();
    //
    //     // Assert
    //     users.Should().BeEmpty();
    // }
    //
    // [ Fact ]
    // public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsersExist()
    // {
    //     // Arrange
    //     var expectedUsers = new[]
    //     {
    //         new User
    //         {
    //             Id       = Guid.NewGuid(),
    //             FullName = "Nick Chapsas"
    //         }
    //     };
    //
    //     //_userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedUsers);
    //     _userRepository.GetAllAsync().Returns(expectedUsers);
    //
    //     // Act
    //     var users = await _sut.GetAllAsync();
    //
    //     // Assert
    //     users.Should().ContainSingle(x => x.FullName == "Nick Chapsas");
    // }

    #endregion
}
