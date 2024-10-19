namespace FunctionalExt;

public record GenericError(string Code, string Message) : Error;