namespace FunctionalExt;

public sealed class ResultUndefinedException : Exception
{
    public ResultUndefinedException() 
        : base("Result is undefined. Create the result using Result.Create method.")
    {}
}