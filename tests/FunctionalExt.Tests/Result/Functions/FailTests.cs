namespace FunctionalExt.Tests.Result;

using static Functions;

public class FailTests
{
    [Fact]
    public void Should_create_failed_result_with_error()
    {
        var error = new GenericError("code", "message");
        var result = Fail<string, GenericError>(error);

        result.Should().BeOfType<Result<string, GenericError>>();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(error);
    }
}
