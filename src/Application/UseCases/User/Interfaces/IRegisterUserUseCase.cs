using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Core.Results;

namespace Application.UseCases.User.Interfaces;

public interface IRegisterUserUseCase
{
    Task<Result<UserResponse>> ExecuteAsync(RegisterUserRequest request);
}
