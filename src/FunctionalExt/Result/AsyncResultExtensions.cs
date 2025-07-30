namespace FunctionalExt;

public static partial class AsyncResultExtensions
{
    /// <summary>
    /// Given a successful input result, applies <paramref name="mapFnAsync"/> async function to <typeparamref name="A"/> value which returns a task-based <typeparamref name="B"/> value.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// Result<HttpResponseMessage> result = await GetUrl("http://some.url");
    /// result.MapAsync(response => response.Contents.ReadAsStringAsync()); // Returns Task<Result<string>>
    /// ]]>
    /// </code>
    /// </example>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="result">The input result.</param>
    /// <param name="mapFnAsync">The mapping function. This receives the unwrapped value of the input result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="mapFnAsync"/> when success or the error wrapped on the input.</returns>
    public static async Task<Result<B, TError>> MapAsync<A, B, TError>(this Result<A, TError> result, Func<A, Task<B>> mapFnAsync) where TError : Error =>
        result.IsSuccess
            ? Result<B, TError>.CreateSuccess(await mapFnAsync(result.Value!))
            : Result<B, TError>.CreateFail(result.Error!);

    /// <summary>
    /// Given a successful input result, applies <paramref name="mapFnAsync"/> async function to <typeparamref name="A"/> value which returns a task-based <typeparamref name="B"/> value.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// var result = GetUrl("http://some.url"); // Returns Task<Result<HttpResponseMessage>>
    /// result.MapAsync(response => response.Contents.ReadAsStringAsync()); // Returns Task<Result<string>>
    /// ]]>
    /// </code>
    /// </example>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="resultTask">The input result.</param>
    /// <param name="mapFnAsync">The mapping function.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="mapFnAsync"/> when success or the error wrapped in the input.</returns>
    public static async Task<Result<B, TError>> MapAsync<A, B, TError>(this Task<Result<A, TError>> resultTask, Func<A, Task<B>> mapFnAsync) where TError : Error
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess
            ? Result<B, TError>.CreateSuccess(await mapFnAsync(result.Value!))
            : Result<B, TError>.CreateFail(result.Error!);
    }

    /// <summary>
    /// Given a successful input result, executes <paramref name="bindFn"/> function which takes unwrapped value of type A and returns a Result of type B.
    /// It returns a new failed Result wrapping the error wrapped on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">The type of the error wrapped in the input result.</typeparam>
    /// <param name="resultTask">The input task-based result.</param>
    /// <param name="bindFn">The binding function. This receives the unwrapped value of the input result and should output a new Result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="bindFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined.</exception>
    public static async Task<Result<B, TError>> BindAsync<A, B, TError>(this Task<Result<A, TError>> resultTask, Func<A, Result<B, TError>> bindFn) where TError : Error =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } result => bindFn(result.Value!),
            { IsUndefined: false, IsSuccess: false } result => Result<B, TError>.CreateFail(result.Error!),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    /// <summary>
    /// Given a successful input result, executes <paramref name="bindAsyncFn"/> async function which takes unwrapped value of type A and returns a task-based Result of type B.
    /// It returns a new failed Result wrapping the error wrapped on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <typeparam name="TError">Tge type of the error wrapped in the input result.</typeparam>
    /// <param name="resultTask">The input task-based result.</param>
    /// <param name="bindAsyncFn">The binding function. This receives the unwrapped value of the input result and should output a new Result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="bindAsyncFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined.</exception>
    public static async Task<Result<B, TError>> BindAsync<A, B, TError>(this Task<Result<A, TError>> resultTask, Func<A, Task<Result<B, TError>>> bindAsyncFn) where TError : Error =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } successResult => await bindAsyncFn(successResult.Value!).ConfigureAwait(false),
            { IsUndefined: false, IsSuccess: false } faultedResult => Result<B, TError>.CreateFail(faultedResult.Error!),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    /// <summary>
    /// Returns an unwrapped value of type <typeparamref name="A"/>. If the input result is not successful, it returns the value returned by <paramref name="defaultValueAsyncFn"/>.
    /// Otherwise, returns the value wrapped in the input result.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="result"></param>
    /// <param name="defaultValueAsyncFn"></param>
    /// <returns>The unwrapped value of the input result, using the default value function to generate a value in case the result is faulted.</returns>
    /// <exception cref="ResultUndefinedException"></exception>
    public static async Task<A> IfFailAsync<A, TError>(this Result<A, TError> result, Func<Task<A>> defaultValueAsyncFn) where TError : Error =>
        result switch
        {
            { IsUndefined: false, IsSuccess: true } successResult => successResult.Value!,
            { IsUndefined: false, IsSuccess: false } => await defaultValueAsyncFn().ConfigureAwait(false),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    /// <summary>
    /// Returns an unwrapped value of type <typeparamref name="A"/>. If the input result is not successful, it returns the <paramref name="defaultValue"/>.
    /// Otherwise, returns the value wrapped in the input result.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="resultTask"></param>
    /// <param name="defaultValue"></param>
    /// <returns>The unwrapped value of the input result, using the default value provided in case the result is faulted.</returns>
    /// <exception cref="ResultUndefinedException"></exception>
    public static async Task<A> IfFailAsync<A, TError>(this Task<Result<A, TError>> resultTask, A defaultValue) where TError : Error =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } result => result.Value!,
            { IsUndefined: false, IsSuccess: false } => defaultValue,
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    /// <summary>
    /// Returns an unwrapped value of type <typeparamref name="A"/>. If the input result is not successful, it returns the value returned by <paramref name="defaultValueFn"/>.
    /// Otherwise, returns the value wrapped in the input result.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="resultTask"></param>
    /// <param name="defaultValueFn"></param>
    /// <returns>The unwrapped value of the input result, using the default value function to generate a value in case the result is faulted</returns>
    /// <exception cref="ResultUndefinedException"></exception>
    public static async Task<A> IfFailAsync<A, TError>(this Task<Result<A, TError>> resultTask, Func<A> defaultValueFn) where TError : Error =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } result => result.Value!,
            { IsUndefined: false, IsSuccess: false } => defaultValueFn(),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    /// <summary>
    /// Returns an unwrapped value of type <typeparamref name="A"/>. If the input result is not successful, it returns the value returned by <paramref name="defaultValueAsyncFn"/>.
    /// Otherwise, returns the value wrapped in the input result.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="resultTask"></param>
    /// <param name="defaultValueAsyncFn"></param>
    /// <returns>The unwrapped value of the input result, using the default value function to generate a value in case the result is faulted.</returns>
    /// <exception cref="ResultUndefinedException"></exception>
    public static async Task<A> IfFailAsync<A, TError>(this Task<Result<A, TError>> resultTask, Func<Task<A>> defaultValueAsyncFn) where TError : Error =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } result => result.Value!,
            { IsUndefined: false, IsSuccess: false } => await defaultValueAsyncFn().ConfigureAwait(false),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    public static async Task<B> MatchAsync<A, B, TError>(this Task<Result<A, TError>> resultTask, Func<A, Task<B>> succFn, Func<TError, Task<B>> errFn) where TError : Error =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } result => await succFn(result.Value!).ConfigureAwait(false),
            { IsUndefined: false, IsSuccess: false } result => await errFn(result.Error!).ConfigureAwait(false),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    public static async Task<B> MatchAsync<A, B, TError>(this Result<A, TError> result, Func<A, Task<B>> succFn, Func<TError, Task<B>> errFn) where TError : Error =>
        result switch
        {
            { IsUndefined: false, IsSuccess: true } => await succFn(result.Value!).ConfigureAwait(false),
            { IsUndefined: false, IsSuccess: false } => await errFn(result.Error!).ConfigureAwait(false),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };
}
