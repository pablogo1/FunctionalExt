namespace FunctionalExt;

/// <summary>
/// The option (or maybe) monad. 
/// This wraps an object, which may exist (Some) or not (None).
/// </summary>
/// <typeparam name="T">The type of the wrapped object.</typeparam>
public readonly record struct Option<T>
{
    internal readonly bool IsSome { get; init; } = false;
    internal readonly T? Value { get; init; } = default;

    private Option(T? value, bool isSome) => (Value, IsSome) = (value, isSome);

    /// <summary>
    /// Creates a new instance of Option with a provided, non-null, non-default, value.
    /// </summary>
    /// <param name="value">An instance of type T which shall not be null.</param>
    /// <returns>An instance of <seealso cref="Option{T}"/> wrapping an object instance.</returns>
    public static Option<T> Some(T value) => new(value ?? throw new ArgumentNullException(nameof(value)), true);

    /// <summary>
    /// Creates a new instance of Option as None of the provided type.
    /// </summary>
    /// <returns>An instance of <see cref="Option{T}"/> with null as wrapped value.</returns>
    public static Option<T> None() => new(default, false);
}
