namespace FunctionalExt.Tests.Option;

public class SomeFactoryMethodTests
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

    [Fact]
    public void Should_throw_exception_given_a_null_value()
    {
        string? nullString = null;

#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => _ = Option<string>.Some(nullString));
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
