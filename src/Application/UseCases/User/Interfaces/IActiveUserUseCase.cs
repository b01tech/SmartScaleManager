using Core.Results;

namespace Application.UseCases.User.Interfaces;

public interface IActiveUserUseCase
{
    Task<Result<bool>> ExecuteAsync(long id);
}
