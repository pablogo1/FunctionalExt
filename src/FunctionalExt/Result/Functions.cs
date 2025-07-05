namespace FunctionalExt;

public static partial class Functions
{
    /// <summary>
    /// Creates a success result.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped value.</typeparam>
    /// <param name="value">The wrapped value.</param>
    /// <returns>A successful Result instance.</returns>
    public static Result<T, TError> Success<T, TError>(T value) where TError : Error => Result<T, TError>.CreateSuccess(value);
}
