namespace FunctionalExt.Tests.Option;

using static Functions;

public class OrElseTests
{
    [Fact]
    public void Should_return_option_value_given_Some()
    {
        const string expectedString = "test";
        Option<string> option = Some(expectedString);
        const int expectedInt = 42;
        Option<int> option2 = Some(expectedInt);

        var result1 = option.OrElse("other");
        var result2 = option.OrElse(() => "other");
        var result3 = option2.OrElse(0);
        var result4 = option2.OrElse(() => 0);

        result1.Should().Be(expectedString);
        result2.Should().Be(expectedString);
        result3.Should().Be(expectedInt);
        result4.Should().Be(expectedInt);
    }

    [Fact]
    public void Should_return_default_value_provided_given_None()
    {
        const string expectedString = "test";
        Option<string> option = None<string>();
        const int expectedInt = 42;
        Option<int> option2 = None<int>();

        var result1 = option.OrElse(expectedString);
        var result2 = option.OrElse(() => expectedString);
        var result3 = option2.OrElse(expectedInt);
        var result4 = option2.OrElse(() => expectedInt);

        result1.Should().Be(expectedString);
        result2.Should().Be(expectedString);
        result3.Should().Be(expectedInt);
        result4.Should().Be(expectedInt);
    }
}