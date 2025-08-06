namespace FunctionalExt;

public static partial class Functions
{
    /// <summary>
    /// Creates an Option with a value (Some)
    /// </summary>
    /// <typeparam name="T">The type of the wrapped object.</typeparam>
    /// <param name="value">The value/instance of the wrapped object.</param>
    /// <returns>An <see cref="Option{T}"/> instance.</returns>
    public static Option<T> Some<T>(T value) => Option<T>.Some(value);
    
    /// <summary>
    /// Creates an Option with no value (None)
    /// </summary>
    /// <typeparam name="T">The type of the wrapped object.</typeparam>
    /// <returns>An <see cref="Option{T}"/> instance.</returns>
    public static Option<T> None<T>() => Option<T>.None();
}