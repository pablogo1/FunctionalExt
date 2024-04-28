namespace FunctionalExt.Tests;

using static Functions;

public class BindTests
{
    [Fact]
    public void Should_return_Option_returned_by_fuction_given_Some()
    {
        Option<double> SafeDivision(double a, double b) => b == 0 ? None<double>() : Some(a / b);

        Option<double> option = Some(10.0);

        var result1 = option.Bind(x => SafeDivision(x, 0));
        var result2 = option.Bind(x => SafeDivision(x, 1));

        result1.Should().BeOfType<Option<double>>();
        result1.IsSome.Should().BeFalse();
        result2.Should().BeOfType<Option<double>>();
        result2.IsSome.Should().BeTrue();

    }

    [Fact]
    public void Should_return_None_given_None()
    {
        Option<string> option = None<string>();

        var result = option.Bind(str => Some(str.Length)); 

        result.Should().BeOfType<Option<int>>();
        result.IsSome.Should().BeFalse();
    }
}
