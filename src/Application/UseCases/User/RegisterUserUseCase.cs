using System.Net;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.UseCases.User.Interfaces;
using Core.Results;

namespace Application.UseCases.User;

internal class RegisterUserUseCase : IRegisterUserUseCase
{
    public Task<Result<UserResponse>> ExecuteAsync(RegisterUserRequest request)
    {
        var userResult = Core.Entities.User.Create(name: request.Name, hashPassword: request.Password, role: request.Role);
        if (!userResult.IsSuccess)
            return Task.FromResult(Result<UserResponse>.Failure(userResult.ErrorResponse.Errors, (int)HttpStatusCode.BadRequest));
        var user = userResult.Data;
        return Task.FromResult<Result<UserResponse>>(new UserResponse(user!.Username.Value));
    }
}
