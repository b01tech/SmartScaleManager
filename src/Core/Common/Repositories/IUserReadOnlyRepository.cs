using Core.Entities;

namespace Core.Common.Repositories;

public interface IUserReadOnlyRepository
{
    Task<bool> UserExists(string username);
    Task<bool> UserExists(long id);
    Task<User?> GetById(long id);
}
