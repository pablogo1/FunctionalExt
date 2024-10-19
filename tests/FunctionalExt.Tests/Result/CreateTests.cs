namespace FunctionalExt.Tests.Result;

public class CreateTests
{
    [Fact]
    public void Should_not_return_an_undefined_result()
    {
        Error error = new GenericError("ErrorCode", "Message");

        var successResult = Result<string>.Create("test");
        var failedResult = Result<string>.Create(error);

        successResult.IsUndefined.Should().BeFalse();
        failedResult.IsUndefined.Should().BeFalse();
    }

    [Fact]
    public void Should_create_result_as_success_given_a_value()
    {
        const string expectedString = "test";
        var result = Result<string>.Create(expectedString);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedString);
    }

    [Fact]
    public void Should_create_result_as_failure_when_given_an_error()
    {
        Error error = new GenericError("ErrorCode", "Message");
        
        var result = Result<string>.Create(error);

        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.Error.Should().Be(error);
    }
}
