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
        
}
