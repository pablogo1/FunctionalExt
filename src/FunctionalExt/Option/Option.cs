namespace FunctionalExt;

public readonly record struct Option<T>
{
    internal readonly bool IsSome { get; init; } = false;
    internal readonly T? Value { get; init; } = default;

    private Option(T? value, bool isSome) => (Value, IsSome) = (value, isSome);

    /// <summary>
    /// Creates a new instance of Option with a provided, non-null, non-default, value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<T> Some(T value) => new(value ?? throw new ArgumentNullException(nameof(value)), true);
    public static Option<T> None() => new(default, false);
}
