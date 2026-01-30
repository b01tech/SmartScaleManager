namespace Core.Results;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public ErrorResponse ErrorResponse { get; }

    private Result(bool isSuccess, T? data, IEnumerable<string> errors, int? statusCode = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorResponse = new ErrorResponse(statusCode ?? 400, errors.ToList());
    }

    public static Result<T> Success(T value) => new(true, value, []);

    public static Result<T> Failure(IEnumerable<string> errors, int? statusCode = null) =>
        new(false, default, errors, statusCode);
    public static Result<T> Failure(string error, int? statusCode = null) =>
        new(false, default, [error], statusCode);

    public static implicit operator Result<T>(T value) => Success(value);
}
