using System.Reflection;
using System.Runtime;

namespace FunctionalExt;

public abstract record Error(string Code, string Message);

public readonly record struct Result<T>
{
    internal readonly bool IsSuccess { get; init; } = false;
    internal readonly T? Value { get; init; } = default;
    internal readonly Error? Error { get; init;} = default;

    private Result(T? value, Error? error, bool isSuccess) => (Value, IsSuccess, Error) = (value, isSuccess, error);

    public static Result<T> Success(T value) => new(value, default, true);
    public static Result<T> Failure(Error error) => new(default, error, false);
}
