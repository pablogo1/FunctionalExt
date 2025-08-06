namespace FunctionalExt.Tests.Option;

public class NoneFactoryMethodTests
{
    [Fact]
    public void Should_create_option_as_none()
    {
        var option = Option<string>.None();

        option.Value.Should().BeNull();
        option.IsSome.Should().BeFalse();
    }
}
