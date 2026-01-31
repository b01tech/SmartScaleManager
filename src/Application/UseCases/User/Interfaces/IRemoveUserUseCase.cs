using Core.Results;

namespace Application.UseCases.User.Interfaces;

public interface IRemoveUserUseCase
{
    Task<Result<bool>> ExecuteAsync(long id);
}
