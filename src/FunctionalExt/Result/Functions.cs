namespace FunctionalExt;

public static partial class Functions
{
    public static Result<T, Error> Success<T>(T value) => Success<T, Error>(value);
    public static Result<T, TError> Success<T, TError>(T value) where TError : IError => Result<T, TError>.Create(value);
}
