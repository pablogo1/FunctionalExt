namespace FunctionalExt;

/// <summary>
/// Represents a type with a single value, used to indicate the absence of a specific value or as a return type for operations that do not return a result.
/// </summary>
/// <remarks>
/// Commonly used in functional programming to represent a void type in generic contexts.
/// </remarks>
public readonly record struct Unit
{
    public static Unit Default { get; } = new();
    public static Task<Unit> Task { get; } = System.Threading.Tasks.Task.FromResult(Default);
}
