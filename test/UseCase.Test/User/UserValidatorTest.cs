using System.Net;
using Application.Dtos.Requests;
using Application.UseCases.User;
using Core.Common.Repositories;
using Core.Enums;
using Core.Errors;
using Moq;

namespace UseCase.Test.User;

public class UserValidatorTest
{
    private readonly Mock<IUserReadOnlyRepository> _readRepositoryMock;
    private readonly UserValidator _validator;

    public UserValidatorTest()
    {
        _readRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _validator = new UserValidator(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ValidateRequestCredentialsAsync_ShouldReturnSuccess_WhenCredentialsAreValidAndUserDoesNotExist()
    {
        // Arrange
        var request = new RegisterUserRequest("validUser", "validPassword123", Role.Admin);
        _readRepositoryMock.Setup(x => x.UserExists(request.Name)).ReturnsAsync(false);

        // Act
        var result = await _validator.ValidateRequestCredentialsAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("validUser", result.Data.userName.Value);
        Assert.Equal("validPassword123", result.Data.password.Value);
    }

    [Fact]
    public async Task ValidateRequestCredentialsAsync_ShouldReturnFailure_WhenNameIsInvalid()
    {
        // Arrange
        var request = new RegisterUserRequest("", "validPassword123", Role.Admin); // Empty name

        // Act
        var result = await _validator.ValidateRequestCredentialsAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(ErrorMessagesResource.NAME_EMPTY, result.ErrorResponse.Errors);
    }

    [Fact]
    public async Task ValidateRequestCredentialsAsync_ShouldReturnFailure_WhenPasswordIsInvalid()
    {
        // Arrange
        var request = new RegisterUserRequest("validUser", "123", Role.Admin); // Short password

        // Act
        var result = await _validator.ValidateRequestCredentialsAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(ErrorMessagesResource.PASSWORD_INVALID, result.ErrorResponse.Errors);
    }

    [Fact]
    public async Task ValidateRequestCredentialsAsync_ShouldReturnFailure_WhenUserAlreadyExists()
    {
        // Arrange
        var request = new RegisterUserRequest("existingUser", "validPassword123", Role.Admin);
        _readRepositoryMock.Setup(x => x.UserExists(request.Name)).ReturnsAsync(true);

        // Act
        var result = await _validator.ValidateRequestCredentialsAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.ErrorResponse.Status);
        Assert.Contains(ErrorMessagesResource.USER_ALREADY_EXISTS, result.ErrorResponse.Errors);
    }
}
