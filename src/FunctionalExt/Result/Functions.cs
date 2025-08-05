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

    /// <summary>
    /// Executes the specified function and returns a <see cref="Result{T, ExceptionalError}"/>.
    /// If the function executes successfully, returns a successful result.
    /// If an exception is thrown, returns a failed result containing an <see cref="ExceptionalError"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by the function.</typeparam>
    /// <param name="func">The function to execute.</param>
    /// <returns>
    /// A <see cref="Result{T, ExceptionalError}"/> representing either the successful result or the exception.
    /// </returns>
    public static Result<T, ExceptionalError> Try<T>(Func<T> func)
    {
        try
        {
            return Success<T, ExceptionalError>(func());
        }
        catch (Exception ex)
        {
            return Fail<T, ExceptionalError>(new ExceptionalError(ex));
        }
    }

    /// <summary>
    /// Executes the specified asynchronous function and returns a <see cref="Result{T, ExceptionalError}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the asynchronous function.</typeparam>
    /// <param name="funcAsync">A function that returns a <see cref="Task{T}"/> representing the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="Task{Result{T, ExceptionalError}}"/> that represents the result of the asynchronous operation.
    /// If the operation completes successfully, returns a successful <see cref="Result{T, ExceptionalError}"/> containing the result.
    /// If an exception is thrown, returns a failed <see cref="Result{T, ExceptionalError}"/> containing an <see cref="ExceptionalError"/>.
    /// </returns>
    public static async Task<Result<T, ExceptionalError>> TryAsync<T>(Func<Task<T>> funcAsync)
    {
        try
        {
            var result = await funcAsync().ConfigureAwait(false);
            return Success<T, ExceptionalError>(result);
        }
        catch (Exception ex)
        {
            return Fail<T, ExceptionalError>(new ExceptionalError(ex));
        }
    }
}
