namespace FunctionalExt.Tests.Option;

public class SomeTests
{
    [Fact]
    public void Should_create_option_with_reference_instance()
    {
        var option = Option<string>.Some("some");

        option.Value.Should().NotBeNull();
        option.IsSome.Should().BeTrue();
    }

    [Fact]
    public void Should_create_option_with_value()
    {
        const int number = 1;
        var option = Option<int>.Some(number);

        option.Value.Should().Be(number);
        option.IsSome.Should().BeTrue();
    }
}
