using System.Collections;

namespace FunctionalExt;

public struct Option<T>
{
    private readonly bool _isSome = false;
    internal T? Value = default;

    private Option(T? value, bool IsSome) => (Value, _isSome) = (value, IsSome);

    public static Option<T> Some(T value) => new(value, true);
    public static Option<T> None() => new(default, false);

    internal readonly bool IsSome => _isSome;

    public readonly Option<B> Map<B>(Func<T, B> map) => IsSome ? Option<B>.Some(map(Value!)) : Option<B>.None();

    public readonly Option<B> Bind<B>(Func<T, Option<B>> fn) => IsSome ? fn(Value!) : Option<B>.None();

    public readonly T IfNone(T orElse) => IsSome ? Value! : orElse;
    public readonly T IfNone(Func<T> orElseFn) => IsSome ? Value! : orElseFn();
}
