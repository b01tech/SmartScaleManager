using System.Net;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Services.Encrypter;
using Application.UseCases.User.Interfaces;
using Core.Common.Repositories;
using Core.Errors;
using Core.Results;
using Core.ValueObjects;

namespace Application.UseCases.User;

internal class RegisterUserUseCase(
    IUserWriteRepository writeRepository,
    IUserReadOnlyRepository readOnlyRepository,
    IUnitOfWork unitOfWork,
    IEncrypter encrypter)
    : IRegisterUserUseCase
{
    public async Task<Result<UserResponse>> ExecuteAsync(RegisterUserRequest request)
    {
        var credentialsResult = await ValidateRequestCredentialsAsync(request);

        if (!credentialsResult.IsSuccess)
            return Result<UserResponse>.Failure(credentialsResult.ErrorResponse.Errors,
                credentialsResult.ErrorResponse.Status);

        var hashPassword = encrypter.Encrypt(credentialsResult.Data!.password.Value);
        var userName = credentialsResult.Data!.userName.Value;

        var userResult = Core.Entities.User.Create(name: userName, hashPassword: hashPassword, role: request.Role);
        if (!userResult.IsSuccess)
            return Result<UserResponse>.Failure(userResult.ErrorResponse.Errors, (int)HttpStatusCode.BadRequest);
        var user = await writeRepository.RegisterUser(userResult.Data!);
        await unitOfWork.CommitAsync();

        return new UserResponse(user.Data.Username.Value);
    }

    private async Task<Result<(Password password, UserName userName)>> ValidateRequestCredentialsAsync(
        RegisterUserRequest request)
    {
        var passwordResult = Password.Create(request.Password);
        var userNameResult = UserName.Create(request.Name);

        var errors = new List<string>();
        if (!userNameResult.IsSuccess)
            errors.AddRange(userNameResult.ErrorResponse.Errors);

        if (!passwordResult.IsSuccess)
            errors.AddRange(passwordResult.ErrorResponse.Errors);

        if (errors.Count > 0)
            return Result<(Password password, UserName userName)>.Failure(errors, (int)HttpStatusCode.BadRequest);

        var userExists = await readOnlyRepository.UserExists(userNameResult.Data!.Value);

        if (!userExists) return (passwordResult.Data!, userNameResult.Data!);

        errors.Add(ErrorMessagesResource.USER_ALREADY_EXISTS);
        return Result<(Password password, UserName userName)>.Failure(errors, (int)HttpStatusCode.Conflict);
    }
}
