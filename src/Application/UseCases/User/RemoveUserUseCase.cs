using System.Net;
using Application.UseCases.User.Interfaces;
using Core.Common.Repositories;
using Core.Errors;
using Core.Results;

namespace Application.UseCases.User;

internal class RemoveUserUseCase(
    IUserReadOnlyRepository readRepository,
    IUserWriteRepository writeRepository,
    IUnitOfWork unitOfWork) : IRemoveUserUseCase
{
    public async Task<Result<bool>> ExecuteAsync(long id)
    {
        var exists = await readRepository.UserExists(id);
        if (!exists)
            return Result<bool>.Failure(ErrorMessagesResource.USER_NOT_FOUND, (int)HttpStatusCode.NotFound);
        await writeRepository.SoftDelete(id);
        await unitOfWork.CommitAsync();
        return Result<bool>.Success(true);
    }
}
