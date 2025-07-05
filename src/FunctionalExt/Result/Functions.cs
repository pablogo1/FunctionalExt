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

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error"The rror.></param>
    /// <returns>A failed Result instance.</returns>
    public static Result<T, TError> Fail<T, TError>(TError error) where TError : Error
        => Result<T, TError>.CreateFail(error);
}
