namespace FunctionalExt;

public static partial class ResultExtensions
{
    public static Result<B, TError> Map<A, B, TError>(this Result<A, TError> result, Func<A, B> mapFn)
        where TError : IError
    {
        return result.IsUndefined 
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? Result<B, TError>.Create(mapFn(result.Value!))
                : Result<B, TError>.Create(result.Error!);
    }

    public static Result<B, TError> Bind<A, B, TError>(this Result<A, TError> result, Func<A, Result<B, TError>> bindFn)
        where TError : IError
    {
        return result.IsUndefined 
            ? throw new ResultUndefinedException()
            : result.IsSuccess 
                ? bindFn(result.Value!)
                : Result<B, TError>.Create(result.Error!);
    }
}