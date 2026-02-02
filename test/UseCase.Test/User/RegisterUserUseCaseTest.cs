using Application.Dtos.Requests;
using Application.Services.Encrypter;
using Application.UseCases.User;
using Application.UseCases.User.Interfaces;
using Core.Common.Repositories;
using Core.Enums;
using Core.Results;
using Core.ValueObjects;
using Moq;

namespace UseCase.Test.User;

public class RegisterUserUseCaseTest
{
    private readonly Mock<IUserWriteRepository> _writeRepositoryMock;
    private readonly Mock<IUserValidator> _validatorMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEncrypter> _encrypterMock;
    private readonly RegisterUserUseCase _useCase;

    public RegisterUserUseCaseTest()
    {
        _writeRepositoryMock = new Mock<IUserWriteRepository>();
        _validatorMock = new Mock<IUserValidator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _encrypterMock = new Mock<IEncrypter>();
        _useCase = new RegisterUserUseCase(
            _writeRepositoryMock.Object,
            _validatorMock.Object,
            _unitOfWorkMock.Object,
            _encrypterMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterUserRequest("validUser", "validPassword", Role.Admin);
        var password = Password.Create("validPassword").Data!;
        var userName = UserName.Create("validUser").Data!;

        _validatorMock.Setup(x => x.ValidateRequestCredentialsAsync(request))
            .ReturnsAsync(Result<(Password, UserName)>.Success((password, userName)));

        _encrypterMock.Setup(x => x.Encrypt(It.IsAny<string>()))
            .Returns("encryptedPassword");

        var createdUser = Core.Entities.User.Create("validUser", "encryptedPassword", Role.Admin).Data!;
        _writeRepositoryMock.Setup(x => x.RegisterUser(It.IsAny<Core.Entities.User>()))
            .ReturnsAsync(Result<Core.Entities.User>.Success(createdUser));

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("validUser", result.Data!.Name);
        _writeRepositoryMock.Verify(x => x.RegisterUser(It.IsAny<Core.Entities.User>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFailure_WhenValidationFails()
    {
        // Arrange
        var request = new RegisterUserRequest("invalid", "short", Role.Admin);

        _validatorMock.Setup(x => x.ValidateRequestCredentialsAsync(request))
            .ReturnsAsync(Result<(Password, UserName)>.Failure("Validation error"));

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Validation error", result.ErrorResponse.Errors);
        _writeRepositoryMock.Verify(x => x.RegisterUser(It.IsAny<Core.Entities.User>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
    }
}
