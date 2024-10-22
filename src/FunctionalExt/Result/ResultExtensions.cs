namespace FunctionalExt;

public static partial class ResultExtensions
{
    /// <summary>
    /// Given a successful input result, executes <paramref name="mapFn"/> function which maps wrapped instance of type A into an instance of type B
    /// and wraps it into a successful result. It returns a new failed <see cref="Result{B}"/> wrapping the error contained on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="mapFn">The mapping function. This receives the unwrapped value of the input result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="mapFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input result is undefined.</exception>
    public static Result<B> Map<A, B>(this Result<A> result, Func<A, B> mapFn) => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? Result<B>.Create(mapFn(result.Value!))
                : Result<B>.Create(result.Error!);

    /// <summary>
    /// Given a successful input result, executes <paramref name="bindFn"/> function which takes unwrapped value of type A and returns a Result of type B.
    /// It returns a new failed Result wrapping the error wrapped on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="bindFn">The binding function. This receives the unwrapped value of the input result and should output a new Result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="bindFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined.</exception>
    public static Result<B> Bind<A, B>(this Result<A> result, Func<A, Result<B>> bindFn) => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? bindFn(result.Value!)
                : Result<B>.Create(result.Error!);

    /// <summary>
    /// Returns an unwrapped value of type <typeparamref name="A"/>. If the input result is successful returns the value returned by <paramref name="succFn"/>.
    /// Otherwise, returns the value returned by <paramref name="errFn"/>.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="succFn">The function executed if input is successful.</param>
    /// <param name="errFn">The function executed if input is faulted.</param>
    /// <returns>An unwrapped value of type <typeparamref name="B"/>.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined</exception>
    public static B Match<A, B>(this Result<A> result, Func<A, B> succFn, Func<Error, B> errFn) =>
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? succFn(result.Value!)
                : errFn(result.Error!);
}
