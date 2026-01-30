using Core.Common.Repositories;
using Core.Entities;
using Core.Results;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

internal class UserRepository(ScaleAppDbContext dbContext) : IUserWriteRepository, IUserReadOnlyRepository
{
    public async Task<Result<User>> RegisterUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        return Result<User>.Success(user);
    }

    public async Task<bool> UserExists(string username)
    {
        return await dbContext.Users.AnyAsync(u => u.Username.Value.Equals(username));
    }
}
