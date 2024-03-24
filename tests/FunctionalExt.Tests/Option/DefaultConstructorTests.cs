namespace FunctionalExt.Tests.Option;

public class DefaultConstructorTests
{
    [Fact]
    public void Should_create_as_None()
    {
        var option = new Option<string>();

        option.Value.Should().BeNull();
        option.IsSome.Should().BeFalse();
    }
}