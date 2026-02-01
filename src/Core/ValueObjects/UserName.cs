using Core.Errors;
using Core.Results;

namespace Core.ValueObjects;

public class UserName
{
    public string Value { get; }
    private const uint MinLenght = 3;

    private UserName(string value)
    {
        Value = value;
    }

    public static Result<UserName> Create(string input)
    {
        var value = input.Trim();
        if (string.IsNullOrWhiteSpace(value))
            return Result<UserName>.Failure(ErrorMessagesResource.NAME_EMPTY);
        if (value.Length < MinLenght)
            return Result<UserName>.Failure(ErrorMessagesResource.NAME_INVALID);

        return new UserName(value);
    }
}
