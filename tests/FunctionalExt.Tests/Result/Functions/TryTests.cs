namespace FunctionalExt.Tests.Result;

using static Functions;

public class TryTests
{
    [Fact]
    public void Should_return_success_result_on_successful_function_execution()
    {
        var result = Try(() => "success");

        result.Should().BeOfType<Result<string, ExceptionalError>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("success");
    }

    [Fact]
    public void Should_return_failed_result_on_exception()
    {
        var exception = new InvalidOperationException("Test exception");
        var result = Try<string>(() => throw exception);

        result.Should().BeOfType<Result<string, ExceptionalError>>();
        result.IsSuccess.Should().BeFalse();
        result.Error!.InnerException.Should().Be(exception);
    }
}
