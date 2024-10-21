namespace FunctionalExt.Tests.Result;

public class MatchTests
{
    private const int SuccessValue = 10;
    private const int FailedValue = -1;

    private static int ExecuteTest(Result<string> result) => result.Match(
        str => SuccessValue,
        err => FailedValue
    );

    [Fact]
    public void Should_return_succFn_value_when_result_is_success()
    {
        var inputResult = Result<string>.Create ("ten");

        int output = ExecuteTest(inputResult);

        output.Should().Be(SuccessValue);
    }

    [Fact]
    public void Should_return_errFn_value_when_result_is_faulted()
    {
        var error = new GenericError ("Error", "Test Error");
        var inputResult = Result<string>.Create(error);

        int output = ExecuteTest(inputResult);

        output.Should().Be(FailedValue);
    }

    [Fact]
    public void Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
    {
        var inputResult = new Result<string>();

        Assert.Throws<ResultUndefinedException>(() => ExecuteTest(inputResult));
    }
}
