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

    public async Task SoftDelete(long id)
    {
        var user = await dbContext.Users.FindAsync(id);
        user?.Deactivate();
    }

    public async Task Activate(long id)
    {
        var user = await dbContext.Users.FindAsync(id);
        user?.Activate();
    }

    public async Task<bool> UserExists(string username)
    {
        return await dbContext.Users.AnyAsync(u => u.Username.Value.Equals(username));
    }

    public async Task<bool> UserExists(long id)
    {
        return await dbContext.Users.AnyAsync(u => u.Id.Equals(id));
    }

    public async Task<User?> GetById(long id)
    {
        return await dbContext.Users.FindAsync(id);
    }
}
