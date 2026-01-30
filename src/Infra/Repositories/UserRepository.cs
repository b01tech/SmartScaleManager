using Core.Common.Repositories;
using Core.Entities;
using Core.Results;
using Infra.Data;

namespace Infra.Repositories;

internal class UserRepository(ScaleAppDbContext dbContext) : IUserWriteRepository
{
    public async Task<Result<User>> RegisterUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        return Result<User>.Success(user);
    }
}
