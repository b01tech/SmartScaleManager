using Application.Dtos.Requests;
using Core.Results;
using Core.ValueObjects;

namespace Application.UseCases.User.Interfaces;

public interface IUserValidator
{
    Task<Result<(Password password, UserName userName)>> ValidateRequestCredentialsAsync(RegisterUserRequest request);
}
