namespace FunctionalExt.Tests.Option;

using static Functions;

public class ToResultTests
{
    [Fact]
    public void Should_return_Success_given_Some()
    {
        Option<string> option = Some("test");

        var result = option.ToResult(new GenericError("Error", "Option was None"));
        var result1 = option.ToResult(() => new GenericError("Error", "Option was None"));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("test");
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("test");
    }

    [Fact]
    public void Should_return_Failure_given_None()
    {
        Option<string> option = None<string>();

        var result = option.ToResult(new GenericError("Error", "Option was None"));
        var result2 = option.ToResult(() => new GenericError("Error", "Option was None"));

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(new GenericError("Error", "Option was None"));
        result2.IsSuccess.Should().BeFalse();
        result2.Error.Should().Be(new GenericError("Error", "Option was None"));
    }
}
