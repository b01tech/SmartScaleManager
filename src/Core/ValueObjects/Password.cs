using Core.Errors;
using Core.Results;

namespace Core.ValueObjects;

public class Password
{
    public string Value { get; private set; } = string.Empty;
    private const uint MinLenght = 6;

    public Password(string value)
    {
        Value = value;
    }

    public static Result<Password> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < MinLenght)
            return Result<Password>.Failure(ErrorMessagesResource.PASSWORD_INVALID);
        return Result<Password>.Success(new Password(value));
    }
}
