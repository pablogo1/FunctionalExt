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

    /// <summary>
    /// Combines two results into one result containing a tuple of their values if both are successful.
    /// If either result is a failure, returns the first encountered failure.
    /// </summary>
    /// <typeparam name="T1">The type of the first result wrapped value.</typeparam>
    /// <typeparam name="T2">The type of the second result wrapped value.</typeparam>
    /// <typeparam name="TError">The type of the error shared across all the results.</typeparam>
    /// <param name="r1">The first result.</param>
    /// <param name="r2">The second result.</param>
    /// <returns>A single result wrapping a tuple containing the values wrapped in the input results.</returns>
    public static Result<(T1, T2), TError> Combine<T1, T2, TError>(Result<T1, TError> r1, Result<T2, TError> r2)
        where TError : Error
    {
        return (r1.IsSuccess, r2.IsSuccess) switch
        {
            (false, _) => Fail<(T1, T2), TError>(r1.Error!),
            (_, false) => Fail<(T1, T2), TError>(r2.Error!),
            _ => Success<(T1, T2), TError>((r1.Value!, r2.Value!))
        };
    }

    public static Result<(T1, T2, T3), TError> Combine<T1, T2, T3, TError>(Result<T1, TError> r1, Result<T2, TError> r2, Result<T3, TError> r3)
        where TError : Error
    {
        return (r1.IsSuccess, r2.IsSuccess, r3.IsSuccess) switch
        {
            (false, _, _) => Fail<(T1, T2, T3), TError>(r1.Error!),
            (_, false, _) => Fail<(T1, T2, T3), TError>(r2.Error!),
            (_, _, false) => Fail<(T1, T2, T3), TError>(r3.Error!),
            _ => Success<(T1, T2, T3), TError>((r1.Value!, r2.Value!, r3.Value!))
        };
    }

    public static Result<(T1, T2, T3, T4), TError> Combine<T1, T2, T3, T4, TError>(Result<T1, TError> r1, Result<T2, TError> r2, Result<T3, TError> r3, Result<T4, TError> r4)
        where TError : Error
    {
        return (r1.IsSuccess, r2.IsSuccess, r3.IsSuccess, r4.IsSuccess) switch
        {
            (false, _, _, _) => Fail<(T1, T2, T3, T4), TError>(r1.Error!),
            (_, false, _, _) => Fail<(T1, T2, T3, T4), TError>(r2.Error!),
            (_, _, false, _) => Fail<(T1, T2, T3, T4), TError>(r3.Error!),
            (_, _, _, false) => Fail<(T1, T2, T3, T4), TError>(r4.Error!),
            _ => Success<(T1, T2, T3, T4), TError>((r1.Value!, r2.Value!, r3.Value!, r4.Value!))
        };
    }

}
