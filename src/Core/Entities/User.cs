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
    public bool IsActive { get; private set; } = true;

    protected User() { }

    private User(UserName username, string hashPassword, Role role)
    {
        Username = username;
        HashPassword = hashPassword;
        Role = role;
    }

    public static Result<User> Create(string name, string hashPassword, Role role)
    {
        var userNameResult = UserName.Create(name);
        if (!userNameResult.IsSuccess)
            return Result<User>.Failure(userNameResult.ErrorResponse.Errors);

        return new User(userNameResult.Data!, hashPassword, role);
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
