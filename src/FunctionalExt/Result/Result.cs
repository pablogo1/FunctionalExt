namespace FunctionalExt;

public readonly record struct Result<T, TError>
    where TError : IError
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
    internal readonly IError? Error { get; init;} = default;

    private Result(T? value, IError? error, ResultState resultState) => (Value, Error, _resultState) = (value, error, resultState);

    public static Result<T, TError> Create(T value) => new(value, default, ResultState.Success);
    public static Result<T, TError> Create(IError error) => new(default, error, ResultState.Faulted);
}
