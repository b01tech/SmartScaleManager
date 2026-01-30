using Core.ValueObjects;
using Core.Results;
using Core.Enums;

namespace Core.Entities;

public class User
{
    public long Id { get; init; }
    public UserName Username { get; private set; } = null!;
    public string HashPassword { get; private set; } = string.Empty;
    public Role Role { get; private set; }

    protected User() { }

    private User(long id, UserName username, string hashPassword, Role role)
    {
        Id = id;
        Username = username;
        HashPassword = hashPassword;
        Role = role;
    }

    public static Result<User> Create(long id, string name, string hashPassword, Role role)
    {
        var userNameResult = UserName.Create(name);
        if (!userNameResult.IsSuccess)
            return Result<User>.Failure(userNameResult.Errors);

        return new User(id, userNameResult.Value!, hashPassword, role);
    }
}
