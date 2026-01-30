namespace Core.Common.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
