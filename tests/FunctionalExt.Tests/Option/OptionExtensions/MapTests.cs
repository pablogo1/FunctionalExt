namespace FunctionalExt.Tests.Option;

using static Functions;

public class MapTests
{
    [Fact]
    public void Should_return_Some_of_B_given_Some()
    {
        var option = Some("test");

        var result = option.Map(str => str.Length);

        result.IsSome.Should().BeTrue();
        result.Value.Should().Be(4);
    }

    [Fact]
    public void Should_return_None_of_B_given_None()
    {
        var option = None<string>();

        var result = option.Map(str => str.Length);

        result.IsSome.Should().BeFalse();
    }
}
