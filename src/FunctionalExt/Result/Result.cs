﻿namespace FunctionalExt;

public readonly record struct Result<A>
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
    internal readonly A? Value { get; init; } = default;
    internal readonly Error? Error { get; init;} = default;

    private Result(A? value, Error? error, ResultState resultState) => (Value, Error, _resultState) = (value, error, resultState);

    /// <summary>
    /// Creates a successful result wrapping value of type <typeparamref name="A"/>.
    /// </summary>
    /// <param name="value">The value to be wrapped.</param>
    /// <returns>A new successful result.</returns>
    public static Result<A> Create(A value) => new(value, default, ResultState.Success);

    /// <summary>
    /// Creates a faulted result wrapping error.
    /// </summary>
    /// <param name="error">The error to be contained by the result.</param>
    /// <returns>A new faulted result.</returns>
    public static Result<A> Create(Error error) => new(default, error, ResultState.Faulted);
}
