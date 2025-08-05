namespace FunctionalExt;

/// <summary>
/// Error type which can be used as a generic error. It is recommended that specific error types are defined.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
public record GenericError(string Code, string Message) : Error
{
    public override string ToString() => $"{Code}: {Message}";
}