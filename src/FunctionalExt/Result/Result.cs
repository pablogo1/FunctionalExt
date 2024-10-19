namespace FunctionalExt;

public readonly record struct Result<T>
{
    private enum ResultState
    {
        Undefined,
        Success,
        Faulted
    }

    private readonly ResultState _resultState = ResultState.Undefined;
    internal readonly bool IsUndefined => _resultState == ResultState.Undefined;
    internal readonly bool IsSuccess => _resultState == ResultState.Success;
    internal readonly T? Value { get; init; } = default;
    internal readonly Error? Error { get; init;} = default;

    private Result(T? value, Error? error, ResultState resultState) => (Value, Error, _resultState) = (value, error, resultState);

    public static Result<T> Create(T value) => new(value, default, ResultState.Success);
    public static Result<T> Create(Error error) => new(default, error, ResultState.Faulted);
}
