using System.Net;
using Application.UseCases.User;
using Core.Common.Repositories;
using Core.Enums;
using Core.Errors;
using Moq;

namespace UseCase.Test.User;

public class GetUserUseCaseTest
{
    private readonly Mock<IUserReadOnlyRepository> _readRepositoryMock;
    private readonly GetUserUseCase _useCase;

    public GetUserUseCaseTest()
    {
        _readRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _useCase = new GetUserUseCase(_readRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var id = 1L;
        var user = Core.Entities.User.Create("validUser", "hash", Role.Admin).Data;

        _readRepositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(user);

        // Act
        var result = await _useCase.ExecuteAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("validUser", result.Data!.Name);
        Assert.Equal(Role.Admin.ToString(), result.Data.Role);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var id = 1L;
        _readRepositoryMock.Setup(x => x.GetById(id)).ReturnsAsync((Core.Entities.User?)null);

        // Act
        var result = await _useCase.ExecuteAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorResponse.Status);
        Assert.Contains(ErrorMessagesResource.USER_NOT_FOUND, result.ErrorResponse.Errors);
    }
}
