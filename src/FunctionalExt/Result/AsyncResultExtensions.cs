namespace FunctionalExt;

public static partial class AsyncResultExtensions
{
    /// <summary>
    /// Map <seealso cref="ResultExtensions.Map{A, B}(Result{A}, Func{A, B})"/> for Task-based result.
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <param name="resultTask">The input task-based result.</param>
    /// <param name="mapFn"></param>
    /// <returns>A task-based result of type <typeparamref name="B"/>.</returns>
    public static async Task<Result<B>> MapAsync<A, B>(this Task<Result<A>> resultTask, Func<A, B> mapFn) =>
        (await resultTask.ConfigureAwait(false)).Map(mapFn);
}
