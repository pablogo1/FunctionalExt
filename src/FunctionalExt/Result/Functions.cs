namespace FunctionalExt;

public static partial class Functions
{
    public static Result<T> Success<T>(T value) => Result<T>.Create(value);
}
