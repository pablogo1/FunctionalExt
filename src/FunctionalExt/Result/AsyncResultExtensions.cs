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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="A"></typeparam>
    /// <typeparam name="B"></typeparam>
    /// <param name="result"></param>
    /// <param name="mapFnAsync"></param>
    /// <returns></returns>
    public static async Task<Result<B>> MapAsync<A, B>(this Result<A> result, Func<A, Task<B>> mapFnAsync) =>
        result.IsSuccess
            ? Result<B>.Create(await mapFnAsync(result.Value!))
            : Result<B>.Create(result.Error!);
}
