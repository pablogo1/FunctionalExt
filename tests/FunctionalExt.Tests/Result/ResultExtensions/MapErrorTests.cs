namespace FunctionalExt.Tests.Result;

public class MapErrorTests
{
    private record ErrorA(string Message) : Error;
    private record ErrorB(string Message) : Error;

    [Fact]
    public void Should_return_ErrorB_given_a_failed_input_result_with_ErrorA()
    {
        var error = new ErrorA("Message A");
        var inputResult = Result<string, ErrorA>.CreateFail(error);

        var actualResult = inputResult.MapError(err => new ErrorB("New Message B"));

        actualResult.IsSuccess.Should().BeFalse();
        actualResult.Error.Should().BeOfType<ErrorB>();
        actualResult.Error?.Message.Should().Be("New Message B");
    }

    [Fact]
    public void Should_return_successful_result_given_a_successful_input_result()
    {
        var inputResult = Result<string, ErrorA>.CreateSuccess("this is a test");

        var actualResult = inputResult.MapError(err => new ErrorB("New Message B"));

        actualResult.IsSuccess.Should().BeTrue();
        actualResult.Value.Should().Be("this is a test");
    }

    [Fact]
    public async Task Should_return_ErrorB_given_a_failed_task_based_input_result_with_ErrorA()
    {
        var error = new ErrorA("Message A");
        var inputResultTask = Task.FromResult(Result<string, ErrorA>.CreateFail(error));

        var actualResult = await inputResultTask.MapError(err => new ErrorB("New Message B"));

        actualResult.IsSuccess.Should().BeFalse();
        actualResult.Error.Should().BeOfType<ErrorB>();
        actualResult.Error?.Message.Should().Be("New Message B");
    }

    [Fact]
    public async Task Should_return_successful_result_given_a_successful_task_based_input_result()
    {
        var inputResultTask = Task.FromResult(Result<string, ErrorA>.CreateSuccess("this is a test"));

        var actualResult = await inputResultTask.MapError(err => new ErrorB("New Message B"));

        actualResult.IsSuccess.Should().BeTrue();
        actualResult.Value.Should().Be("this is a test");
    }
}
