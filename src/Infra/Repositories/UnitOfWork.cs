using Core.Common.Repositories;
using Infra.Data;

namespace Infra.Repositories;

internal class UnitOfWork(ScaleAppDbContext dbContext) : IUnitOfWork
{
    public async Task CommitAsync() => await dbContext.SaveChangesAsync();
}
