using System.Runtime.InteropServices.JavaScript;

namespace Core.Results;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public IEnumerable<string> Errors { get; } = [];
    public int? StatusCode { get; }

    private Result(bool isSuccess, T? value, IEnumerable<string> errors, int? statusCode = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value) => new(true, value, []);

    public static Result<T> Failure(IEnumerable<string> errors, int? statusCode = null) =>
        new(false, default, errors, statusCode);
    public static Result<T> Failure(string error, int? statusCode = null) =>
        new(false, default, [error], statusCode);

    public static implicit operator Result<T>(T value) => Success(value);
}
