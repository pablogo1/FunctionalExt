namespace FunctionalExt;

public record Error(string Code, string Message) : IError;
