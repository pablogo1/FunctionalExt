namespace FunctionalExt.Tests.Result;

using static Functions;

public class TryAsyncTests
{
    [Fact]
    public async Task Should_return_success_result_on_successful_async_function_execution()
    {
        var result = await TryAsync(() => Task.FromResult("success"));

        result.Should().BeOfType<Result<string, ExceptionalError>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("success");
    }

    [Fact]
    public async Task Should_return_failed_result_on_exception_in_async_function()
    {
        var exception = new InvalidOperationException("Test exception");
        var result = await TryAsync<string>(() => Task.FromException<string>(exception));

        result.Should().BeOfType<Result<string, ExceptionalError>>();
        result.IsSuccess.Should().BeFalse();
        result.Error!.InnerException.Should().Be(exception);
    }
}
