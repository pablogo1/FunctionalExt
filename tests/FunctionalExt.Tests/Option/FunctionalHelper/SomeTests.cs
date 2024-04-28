namespace FunctionalExt.Tests.Option.FunctionalHelper;

using static FunctionalExt.Functions;

public class SomeTests
{
    [Fact]
    public void Should_create_Option_with_value()
    {
        var option = Some("this");

        option.Should().BeOfType<Option<string>>();
        option.Value.Should().Be("this");
        option.IsSome.Should().BeTrue();
    }
}
