namespace FunctionalExt.Tests.Option;

public class DefaultConstructorTests
{
    [Fact]
    public void Should_create_as_None()
    {
        var stringOption = new Option<string>();
        var intOption = new Option<int>();

        stringOption.Value.Should().BeNull();
        stringOption.IsSome.Should().BeFalse();

        intOption.Value.Should().Be(default);
        intOption.IsSome.Should().BeFalse();
    }
}