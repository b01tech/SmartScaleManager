using System.Net;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Services.Encrypter;
using Application.UseCases.User.Interfaces;
using Core.Common.Repositories;
using Core.Results;

namespace Application.UseCases.User;

internal class RegisterUserUseCase(
    IUserWriteRepository writeRepository,
    IUserValidator validator,
    IUnitOfWork unitOfWork,
    IEncrypter encrypter)
    : IRegisterUserUseCase
{
    public async Task<Result<UserResponse>> ExecuteAsync(RegisterUserRequest request)
    {
        var credentialsResult = await validator.ValidateRequestCredentialsAsync(request);

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

        return new UserResponse(user.Data!.Username.Value);
    }
}
