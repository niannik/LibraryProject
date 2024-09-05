namespace Application.Common;

public record Result
{
    public readonly Error? Error;

    public bool IsSucceeded { get; }
    public bool IsFailed => IsSucceeded == false;

    protected Result()
    {
        IsSucceeded = true;
    }
    protected Result(Error error)
    {
        IsSucceeded = false;
        Error = error;
    }

    public static Result Ok() => new();

    public static Result Failed(Error error) => new(error);

    public static implicit operator Result(Error error) => new(error);
}

public record Result<TData> : Result
{
    public readonly TData? Data;
    private Result(TData data)
    {
        Data = data;
    }

    private Result(Error error) : base(error)
    {
    }

    public static Result<TData> Ok(TData value) => new(value);
    public static new Result<TData> Failed(Error error) => new(error);

    public static implicit operator Result<TData>(TData value) => new(value);
    public static implicit operator Result<TData>(Error error) => new(error);
}
