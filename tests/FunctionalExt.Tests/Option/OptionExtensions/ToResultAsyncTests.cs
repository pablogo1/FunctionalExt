namespace FunctionalExt.Tests.Option;

using static Functions;

public class ToResultAsyncTests
{
    [Fact]
    public async Task Should_return_Success_given_Some()
    {
        Task<Option<string>> option = Task.FromResult(Some("test"));

        var result = await option.ToResultAsync(new GenericError("Error", "Option was None"));
        var result2 = await option.ToResultAsync(() => new GenericError("Error", "Option was None"));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("test");
        result2.IsSuccess.Should().BeTrue();
        result2.Value.Should().Be("test");
    }

    [Fact]
    public async Task Should_return_Failure_given_None()
    {
        Task<Option<string>> option = Task.FromResult(None<string>());

        var result = await option.ToResultAsync(new GenericError("Error", "Option was None"));
        var result2 = await option.ToResultAsync(() => new GenericError("Error", "Option was None"));

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(new GenericError("Error", "Option was None"));
        result2.IsSuccess.Should().BeFalse();
        result2.Error.Should().Be(new GenericError("Error", "Option was None"));
    }
}
