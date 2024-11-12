namespace FunctionalExt;

public static class OptionExtensions
{
    /// <summary>
    /// Executes the provided mapping function against the wrapped value if option is Some. Returns None if the input option is none.
    /// </summary>
    /// <typeparam name="T">The input option enclosed type.</typeparam>
    /// <typeparam name="B">The output option enclosed type.</typeparam>
    /// <param name="option">The input option.</param>
    /// <param name="map">The mapping function.</param>
    /// <returns>An <see cref="Option{B}"/> instance.</returns>
    public static Option<B> Map<T, B>(this Option<T> option, Func<T, B> map) => option.IsSome 
        ? Option<B>.Some(map(option.Value!)) 
        : Option<B>.None();

    /// <summary>
    /// Executes the provided function <paramref name="fn"/> if option is some. Returns None if the input option is none.
    /// </summary>
    /// <typeparam name="B"></typeparam>
    /// <param name="fn">A function to execute when option is some. It must return an Option of <typeparamref name="B"/> instance.</param>
    /// <returns>An <see cref="Option{B}"/> instance, either Some or None.</returns>
    public static Option<B> Bind<T, B>(this Option<T> option, Func<T, Option<B>> fn) => option.IsSome 
        ? fn(option.Value!) 
        : Option<B>.None();

    /// <summary>
    /// See <seealso cref="Bind"/>.
    /// </summary>
    /// <typeparam name="B"></typeparam>
    /// <param name="fn"></param>
    /// <returns></returns>
    public static Option<B> FlatMap<T, B>(Option<T> option, Func<T, Option<B>> fn) => option.Bind(fn);

    /// <summary>
    /// Unwraps the Option value, providing a default value in case the Option is None.
    /// </summary>
    /// <param name="orElse">The default value to return in case is None.</param>
    /// <returns>The wrapped value if Some. The provided default value if None.</returns>
    public static T IfNone<T>(this Option<T> option, T orElse) => option.IsSome ? option.Value! : orElse;

    /// <summary>
    /// Unwraps the Option value, providing a default value function (lazy) in case the Option is None.
    /// </summary>
    /// <param name="orElseFn">The function that generates the default value in case the Option is None.</param>
    /// <returns>The wrapped value if Some. The provided default value if None.</returns>
    public static T IfNone<T>(this Option<T> option, Func<T> orElseFn) => option.IsSome ? option.Value! : orElseFn();
}