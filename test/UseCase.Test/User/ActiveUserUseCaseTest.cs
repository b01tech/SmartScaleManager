using System.Net;
using Application.UseCases.User;
using Core.Common.Repositories;
using Core.Errors;
using Moq;

namespace UseCase.Test.User;

public class ActiveUserUseCaseTest
{
    private readonly Mock<IUserReadOnlyRepository> _readRepositoryMock;
    private readonly Mock<IUserWriteRepository> _writeRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ActiveUserUseCase _useCase;

    public ActiveUserUseCaseTest()
    {
        _readRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _writeRepositoryMock = new Mock<IUserWriteRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _useCase = new ActiveUserUseCase(
            _readRepositoryMock.Object,
            _writeRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange
        var id = 1L;
        _readRepositoryMock.Setup(x => x.UserExists(id)).ReturnsAsync(true);

        // Act
        var result = await _useCase.ExecuteAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
        _writeRepositoryMock.Verify(x => x.Activate(id), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var id = 1L;
        _readRepositoryMock.Setup(x => x.UserExists(id)).ReturnsAsync(false);

        // Act
        var result = await _useCase.ExecuteAsync(id);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.ErrorResponse.Status);
        Assert.Contains(ErrorMessagesResource.USER_NOT_FOUND, result.ErrorResponse.Errors);
        _writeRepositoryMock.Verify(x => x.Activate(id), Times.Never);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
    }
}
