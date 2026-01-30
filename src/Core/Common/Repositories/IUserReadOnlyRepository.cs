namespace Core.Common.Repositories;

public interface IUserReadOnlyRepository
{
    Task<bool> UserExists(string username);
}
