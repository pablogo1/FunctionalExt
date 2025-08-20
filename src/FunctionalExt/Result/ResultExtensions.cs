namespace FunctionalExt;

public static partial class ResultExtensions
{
    /// <summary>
    /// Given a successful input result, executes <paramref name="mapFn"/> function which maps wrapped instance of type A into an instance of type B
    /// and wraps it into a successful result. It returns a new failed <see cref="Result{B, TError}"/> wrapping the error contained on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="mapFn">The mapping function. This receives the unwrapped value of the input result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="mapFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input result is undefined.</exception>
    public static Result<B, TError> Map<A, B, TError>(this Result<A, TError> result, Func<A, B> mapFn) where TError : Error => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? Result<B, TError>.CreateSuccess(mapFn(result.Value!))
                : Result<B, TError>.CreateFail(result.Error!);

    /// <summary>
    /// Map <seealso cref="ResultExtensions.Map{A, B, TError}(Result{A, TError}, Func{A, B})"/> for Task-based result.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="resultTask">The input task-based result.</param>
    /// <param name="mapFn"></param>
    /// <returns>A task-based result of type <typeparamref name="B"/>.</returns>
    public static async Task<Result<B, TError>> Map<A, B, TError>(this Task<Result<A, TError>> resultTask, Func<A, B> mapFn) where TError : Error =>
        (await resultTask.ConfigureAwait(false)).Map(mapFn);
        
    /// <summary>
    /// Given a failed input result, executes <paramref name="mapErrorFn"/> function which maps the error of type TErrorA into an error of type TErrorB
    /// and wraps it into a failed result. It returns a new successful <see cref="Result{A, TErrorB}"/> wrapping the value contained on the input result.
    /// </summary>
    public static Result<A, TErrorB> MapError<A, TErrorA, TErrorB>(this Result<A, TErrorA> result, Func<TErrorA, TErrorB> mapErrorFn) where TErrorA : Error where TErrorB : Error =>
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? Result<A, TErrorB>.CreateSuccess(result.Value!)
                : Result<A, TErrorB>.CreateFail(mapErrorFn(result.Error!));

    /// <summary>
    /// Map <seealso cref="ResultExtensions.MapError{A, TErrorA, TErrorB}(Result{A, TErrorA}, Func{TErrorA, TErrorB})"/> for Task-based result.
    /// </summary>
    public static async Task<Result<A, TErrorB>> MapError<A, TErrorA, TErrorB>(this Task<Result<A, TErrorA>> resultTask, Func<TErrorA, TErrorB> mapErrorFn) where TErrorA : Error where TErrorB : Error =>
        (await resultTask.ConfigureAwait(false)).MapError(mapErrorFn);

    /// <summary>
    /// Given a successful input result, executes <paramref name="bindFn"/> function which takes unwrapped value of type A and returns a Result of type B.
    /// It returns a new failed Result wrapping the error wrapped on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="bindFn">The binding function. This receives the unwrapped value of the input result and should output a new Result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="bindFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined.</exception>
    public static Result<B, TError> Bind<A, B, TError>(this Result<A, TError> result, Func<A, Result<B, TError>> bindFn) where TError : Error =>
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? bindFn(result.Value!)
                : Result<B, TError>.CreateFail(result.Error!);

    /// <summary>
    /// Returns an unwrapped value of type <typeparamref name="A"/>. If the input result is successful returns the value returned by <paramref name="success"/>.
    /// Otherwise, returns the value returned by <paramref name="error"/>.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="success">The function executed if input is successful.</param>
    /// <param name="error">The function executed if input is faulted.</param>
    /// <returns>An unwrapped value of type <typeparamref name="B"/>.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined</exception>
    public static B Match<A, B, TError>(this Result<A, TError> result, Func<A, B> success, Func<TError, B> error) where TError : Error =>
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? success(result.Value!)
                : error(result.Error!);

    /// <summary>
    /// Given a faulted error, return the <paramref name="defaultValue"/>. Otherwise, it returns the
    /// wrapped value.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="defaultValue">The default value to return in case of faulted result.</param>
    /// <returns>An unwrapped instance of <typeparamref name="A"/>.</returns>
    public static A IfFail<A, TError>(this Result<A, TError> result, A defaultValue) where TError : Error => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsFaulted
                ? defaultValue
                : result.Value!;

    /// <summary>
    /// Given a faulted error, return the object returned by <paramref name="defaultValueFn"/>. Otherwise,
    /// it returns the wrapped value.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="defaultValueFn">The default value factory function.</param>
    /// <returns>An unwrapped instance of <typeparamref name="A"/>.</returns>
    public static A IfFail<A, TError>(this Result<A, TError> result, Func<A> defaultValueFn) where TError : Error => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsFaulted
                ? defaultValueFn()
                : result.Value!;
            
    /// <summary>
    /// Given a faulted result, return the object return by <paramref name="failFn"/>. Otherwise, it returns the 
    /// wrapped value.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="failFn">The default value factory function.</param>
    /// <returns>An unwrapped instance of <typeparamref name="A"/>.</returns>
    public static A IfFail<A, TError>(this Result<A, TError> result, Func<TError, A> failFn) where TError : Error => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsFaulted
                ? failFn(result.Error!)
                : result.Value!;

    /// <summary>
    /// Given a faulted result, execute the <paramref name="action"/> function.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="action">The action to execute.</param>
    /// <returns>The default <see cref="Unit"/> instance.</returns>
    public static Unit IfFail<A, TError>(this Result<A, TError> result, Action<Error> action) where TError : Error
    {
        if (result.IsUndefined)
        {
            throw new ResultUndefinedException();
        }
        
        if (result.IsFaulted)
        {
            action(result.Error!);
        }

        return Unit.Default;
    }
}
