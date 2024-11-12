namespace FunctionalExt;

/// <summary>
/// Thrown when the Result was created not via the provided static factory methods.
/// </summary>
public sealed class ResultUndefinedException : Exception
{
    public ResultUndefinedException() 
        : base("Result is undefined. Create the result using Result.Create method.")
    {}
}