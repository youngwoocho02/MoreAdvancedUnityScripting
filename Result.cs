using UnityEngine;

public class Result<T>
{
    private Result(bool isSuccess, T value, ErrorDetails error)
    {
        if (isSuccess && error != ErrorDetails.None)
        {
            Debug.LogError("Success result must not have an error.");
        }
        if (!isSuccess && error == ErrorDetails.None)
        {
            Debug.LogError("Failure result must have an error.");
        }

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; } = false;
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public ErrorDetails Error { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, ErrorDetails.None);
    }

    public static Result<T> Failure(ErrorDetails error)
    {
        return new Result<T>(false, default, error);
    }
}

public readonly struct Success
{
}

public record ErrorDetails(string Code, string Message)
{
    public static readonly ErrorDetails None = new(string.Empty, string.Empty);
    public static readonly ErrorDetails NullValue = new("Error.NullValue", "A null value was provided.");
    public static readonly ErrorDetails TooManyResults = new("Error.TooManyResults", "Multiple results were found when only one was expected.");
    public static readonly ErrorDetails NotAvailable = new("Error.NotAvailable", "The requested resource is not available.");
    public static readonly ErrorDetails ExecutionError = new("Error.ExecutionError", "An error occurred during execution.");
    public string Code { get; } = Code;
    public string Message { get; } = Message;
}