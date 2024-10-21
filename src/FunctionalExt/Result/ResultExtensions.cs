namespace FunctionalExt;

public static partial class ResultExtensions
{
    public static Result<B> Map<A, B>(this Result<A> result, Func<A, B> mapFn) => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? Result<B>.Create(mapFn(result.Value!))
                : Result<B>.Create(result.Error!);

    public static Result<B> Bind<A, B>(this Result<A> result, Func<A, Result<B>> bindFn) => 
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? bindFn(result.Value!)
                : Result<B>.Create(result.Error!);

    public static B Match<A, B>(this Result<A> result, Func<A, B> succFn, Func<Error, B> errFn) =>
        result.IsUndefined
            ? throw new ResultUndefinedException()
            : result.IsSuccess
                ? succFn(result.Value!)
                : errFn(result.Error!);
}
