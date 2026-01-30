using Core.Entities;
using Core.Results;

namespace Core.Common.Repositories;

public interface IUserWriteRepository
{
    Task<Result<User>> RegisterUser(User user);
}
