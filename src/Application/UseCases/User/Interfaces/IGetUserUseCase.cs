using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Core.Results;

namespace Application.UseCases.User.Interfaces;

public interface IGetUserUseCase
{
    Task<Result<UserDetailsResponse>> ExecuteAsync(long id);
}
