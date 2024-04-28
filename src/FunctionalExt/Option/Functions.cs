namespace FunctionalExt;

public static partial class Functions
{
    /// <summary>
    /// Creates an Option with a value (Some)
    /// </summary>
    /// <typeparam name="T">The type of the wrapped object.</typeparam>
    /// <param name="value">The value/instance of the wrapped object.</param>
    /// <returns>An <see cref="Option{T}"/> intance.</returns>
    public static Option<T> Some<T>(T value) => Option<T>.Some(value);
    public static Option<T> None<T>() => Option<T>.None();
}