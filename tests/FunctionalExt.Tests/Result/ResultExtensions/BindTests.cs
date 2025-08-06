namespace FunctionalExt.Tests.Result;

public class BindTests
{
    private static Result<int, GenericError> BindFn(string input) => Result<int, GenericError>.CreateSuccess(10);
    private static GenericError CreateError() => new ("Code", "Message");

    [Fact]
    public void Should_return_result_of_type_B_given_by_the_execution_of_bindFn_on_a_successful_result()
    {
        var inputResult = Result<string, GenericError>.CreateSuccess("ten");

        Result<int, GenericError> outResult = inputResult.Bind(BindFn);

        outResult.IsSuccess.Should().BeTrue();
        outResult.Value.Should().Be(10);
    }

    [Fact]
    public void Should_return_failed_result_of_type_B_given_failed_input_result()
    {
        var error = CreateError();
        var inputResult = Result<string, GenericError>.CreateFail(error);

        var outResult = inputResult.Bind(BindFn);

        outResult.IsSuccess.Should().BeFalse();
        outResult.Error.Should().Be(error);
    }

    [Fact]
    public void Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
    {
        var inputResult = new Result<string, GenericError>();

        Assert.Throws<ResultUndefinedException>(() => inputResult.Bind(BindFn));
    }
}
