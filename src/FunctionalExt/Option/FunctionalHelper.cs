namespace FunctionalExt;

public static partial class FunctionalHelper
{
    public static Option<T> Some<T>(T value) => Option<T>.Some(value);
    public static Option<T> None<T>() => Option<T>.None();
}