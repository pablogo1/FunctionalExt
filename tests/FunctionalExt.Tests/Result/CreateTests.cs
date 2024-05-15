namespace FunctionalExt.Tests.Result;

public class CreateTests
{
    [Fact]
    public void Should_create_result_as_success_given_a_value()
    {
        const string expectedString = "test";
        var result = Result<string, Error>.Create(expectedString);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedString);
    }
}
