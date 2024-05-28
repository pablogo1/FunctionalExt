namespace FunctionalExt.Tests.Result;

using static Functions;

public class SuccessTests
{
    [Fact]
    public void Should_create_successful_result_with_value()
    {
        var result = Success("test");

        result.Should().BeOfType<Result<string, Error>>();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("test");
    }
}
