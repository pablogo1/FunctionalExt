namespace FunctionalExt;

public record ExceptionalError(Exception InnerException) : Error
{
    public override string ToString() => $"{nameof(ExceptionalError)} - {InnerException.Message}";
}