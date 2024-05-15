using System.Reflection;
using System.Runtime;

namespace FunctionalExt;

public record Error(string Code, string Message) : IError;
public interface IError {}

enum ResultState
{
    Undefined,
    Success,
    Faulted
}

public readonly record struct Result<T, TError>
    where TError : IError
{
    private readonly ResultState _resultState = ResultState.Undefined;
    internal readonly bool IsUndefined => _resultState == ResultState.Undefined;
    internal readonly bool IsSuccess => _resultState == ResultState.Success;
    internal readonly T? Value { get; init; } = default;
    internal readonly IError? Error { get; init;} = default;

    private Result(T? value, IError? error, ResultState resultState) => (Value, Error, _resultState) = (value, error, resultState);

    public static Result<T, TError> Create(T value) => new(value, default, ResultState.Success);
    public static Result<T, TError> Create(IError error) => new(default, error, ResultState.Faulted);
}
