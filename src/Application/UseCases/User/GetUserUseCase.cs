using System.Net;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.UseCases.User.Interfaces;
using Core.Common.Repositories;
using Core.Errors;
using Core.Results;

namespace Application.UseCases.User;

internal class GetUserUseCase(IUserReadOnlyRepository repository) : IGetUserUseCase
{
    public async Task<Result<UserDetailsResponse>> ExecuteAsync(long id)
    {
        var user = await repository.GetById(id);
        if (user is null)
            return Result<UserDetailsResponse>.Failure(ErrorMessagesResource.USER_NOT_FOUND,
                (int)HttpStatusCode.NotFound);
        return new UserDetailsResponse(
            Id: user.Id,
            Name: user.Username.Value,
            Role: user.Role.ToString(),
            IsActive: user.IsActive);
    }
}
