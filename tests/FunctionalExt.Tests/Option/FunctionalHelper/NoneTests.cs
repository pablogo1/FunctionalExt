namespace FunctionalExt.Tests.Option.FunctionalHelper;

using static FunctionalExt.FunctionalHelper;

public class NoneTests
{
    [Fact]
    public void Should_create_Option_as_None()
    {
        var option = None<string>();

        option.Should().BeOfType<Option<string>>();
        option.Value.Should().BeNull();
        option.IsSome.Should().BeFalse();
    }
}