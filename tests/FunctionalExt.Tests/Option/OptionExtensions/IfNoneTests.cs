namespace FunctionalExt.Tests;

using static Functions;

public class IfNoneTests
{
    [Fact]
    public void Should_return_option_value_given_Some()
    {
        const string expectedString = "test";
        Option<string> option = Some(expectedString);

        var result1 = option.IfNone("other");
        var result2 = option.IfNone(() => "other");

        result1.Should().Be(expectedString);
        result2.Should().Be(expectedString);
    }

    [Fact]
    public void Should_return_default_value_provided_given_None()
    {
        const string expectedString = "test";
        Option<string> option = None<string>();

        var result1 = option.IfNone(expectedString);
        var result2 = option.IfNone(() => expectedString);

        result1.Should().Be(expectedString);
        result2.Should().Be(expectedString);
    }
}
