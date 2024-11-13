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
    /// <param name="result">The input result.</param>
    /// <param name="mapFnAsync">The mapping function. This receives the unwrapped value of the input result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="mapFnAsync"/> when success or the error wrapped on the input.</returns>
    public static async Task<Result<B>> MapAsync<A, B>(this Result<A> result, Func<A, Task<B>> mapFnAsync) =>
        result.IsSuccess
            ? Result<B>.Create(await mapFnAsync(result.Value!))
            : Result<B>.Create(result.Error!);

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
    /// <param name="resultTask">The input result.</param>
    /// <param name="mapFnAsync">The mapping function.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="mapFnAsync"/> when success or the error wrapped in the input.</returns>
    public static async Task<Result<B>> MapAsync<A, B>(this Task<Result<A>> resultTask, Func<A, Task<B>> mapFnAsync) 
    {
        var result = await resultTask.ConfigureAwait(false);
        return result.IsSuccess
            ? Result<B>.Create(await mapFnAsync(result.Value!))
            : Result<B>.Create(result.Error!);
    }
    
    /// <summary>
    /// Given a successful input result, executes <paramref name="bindFn"/> function which takes unwrapped value of type A and returns a Result of type B.
    /// It returns a new failed Result wrapping the error wrapped on the input result.
    /// </summary>
    /// <typeparam name="A">The type of the wrapped value from input result.</typeparam>
    /// <typeparam name="B">The type of the output value.</typeparam>
    /// <param name="resultTask">The input task-based result.</param>
    /// <param name="bindFn">The binding function. This receives the unwrapped value of the input result and should output a new Result.</param>
    /// <returns>A new Result wrapping either the outcome of <paramref name="bindFn"/> when success or the error wrapped on the input.</returns>
    /// <exception cref="ResultUndefinedException">Thrown when the input is undefined.</exception>
    public static async Task<Result<B>> BindAsync<A, B>(this Task<Result<A>> resultTask, Func<A, Result<B>> bindFn) =>
        await resultTask.ConfigureAwait(false) switch {
            { IsUndefined: false, IsSuccess: true } result => bindFn(result.Value!),
            { IsUndefined: false, IsSuccess: false } result => Result<B>.Create(result.Error!),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    public static async Task<A> IfFailAsync<A>(this Result<A> result, Func<Task<A>> defaultValueAsyncFn) =>
        result switch {
            { IsUndefined: false, IsSuccess: true } successResult => successResult.Value!,
            { IsUndefined: false, IsSuccess: false } => await defaultValueAsyncFn().ConfigureAwait(false),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    public static async Task<A> IfFailAsync<A>(this Task<Result<A>> resultTask, A defaultValue) =>
        await resultTask.ConfigureAwait(false) switch 
        {
            { IsUndefined: false, IsSuccess: true } result => result.Value!,
            { IsUndefined: false, IsSuccess: false } => defaultValue,
            { IsUndefined: true } => throw new ResultUndefinedException()
        };

    public static async Task<A> IfFailAsync<A>(this Task<Result<A>> resultTask, Func<A> defaultValueFn) =>
        await resultTask.ConfigureAwait(false) switch
        {
            { IsUndefined: false, IsSuccess: true } result => result.Value!,
            { IsUndefined: false, IsSuccess: false } => defaultValueFn(),
            { IsUndefined: true } => throw new ResultUndefinedException()
        };
}
