namespace FunctionalExt;

public static partial class AsyncResultExtensions
{
    public static async Task<Result<B>> MapAsync<A, B>(this Task<Result<A>> resultTask, Func<A, B> mapFn) =>
        (await resultTask.ConfigureAwait(false)).Map(mapFn);
}
