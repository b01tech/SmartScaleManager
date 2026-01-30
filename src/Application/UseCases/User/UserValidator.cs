using System.Net;
using Application.Dtos.Requests;
using Application.UseCases.User.Interfaces;
using Core.Common.Repositories;
using Core.Errors;
using Core.Results;
using Core.ValueObjects;

namespace Application.UseCases.User;

public class UserValidator(IUserReadOnlyRepository readOnlyRepository) : IUserValidator
{
    public async Task<Result<(Password password, UserName userName)>> ValidateRequestCredentialsAsync(
        RegisterUserRequest request
    )
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
