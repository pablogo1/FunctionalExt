namespace FunctionalExt.Tests.Result;

public class DefaultConstructorTests
{
    [Fact]
    public void Should_create_as_Undefined()
    {
        var result = new Result<string>();

        result.IsUndefined.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeNull();
        result.Value.Should().BeNull();
    }
}
