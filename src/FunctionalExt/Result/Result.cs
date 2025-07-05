namespace FunctionalExt;

/// <summary>
/// The Result monad. This can be either a successful result, which wraps the value, or a failed result which
/// wraps an <seealso cref="Error"/> instance.
/// </summary>
/// <typeparam name="A">The type of the wrapped value.</typeparam>
public readonly record struct Result<A, TError>
    where TError : Error
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
    internal readonly bool IsFaulted => _resultState == ResultState.Faulted;
    internal readonly A? Value { get; init; } = default;
    internal readonly TError? Error { get; init;} = default;

    private Result(A? value, TError? error, ResultState resultState) => (Value, Error, _resultState) = (value, error, resultState);

    /// <summary>
    /// Creates a successful result wrapping value of type <typeparamref name="A"/>.
    /// </summary>
    /// <param name="value">The value to be wrapped.</param>
    /// <returns>A new successful result.</returns>
    public static Result<A, TError> CreateSuccess(A value) => new(value, null, ResultState.Success);

    /// <summary>
    /// Creates a faulted result wrapping error.
    /// </summary>
    /// <param name="error">The error to be contained by the result.</param>
    /// <returns>A new faulted result.</returns>
    public static Result<A, TError> CreateFail(TError error) => new(default, error, ResultState.Faulted);
}
