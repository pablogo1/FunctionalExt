namespace FunctionalExt.Tests.Result;

public class CreateTests
{
    [Fact]
    public void Should_not_return_an_undefined_result()
    {
        Error error = new("ErrorCode", "Message");

        var successResult = Result<string, Error>.Create("test");
        var failedResult = Result<string, Error>.Create(error);

        successResult.IsUndefined.Should().BeFalse();
        failedResult.IsUndefined.Should().BeFalse();
    }

    [Fact]
    public void Should_create_result_as_success_given_a_value()
    {
        const string expectedString = "test";
        var result = Result<string, Error>.Create(expectedString);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedString);
    }

    [Fact]
    public void Should_create_result_as_failure_when_given_an_error()
    {
        Error error = new("ErrorCode", "Message");
        
        var result = Result<string, Error>.Create(error);

        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.Error.Should().Be(error);
    }
}
