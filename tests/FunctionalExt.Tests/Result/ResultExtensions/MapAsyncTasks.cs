namespace FunctionalExt.Tests.Result;

public class MapAsyncTasks
{
    [Fact]
    public async Task Should_return_successful_result_of_type_B_given_a_successful_input_result()
    {
        var inputResultTask = Task.FromResult(Result<string>.Create("Lorem ipsum dolor sit amet"));

        var actualResultTask = inputResultTask.MapAsync(str => str.Split(" "));

        actualResultTask.Should().BeOfType<Task<Result<string[]>>>();
        var actualResult = await actualResultTask;
        actualResult.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_return_failed_result_of_type_B_given_a_faulted_input_result()
    {
        Error error = new GenericError("Code", "Error");
        var inputResultTask = Task.FromResult(Result<string>.Create(error));

        var actualResultTask = inputResultTask.MapAsync(str => str.Split(" "));

        actualResultTask.Should().BeOfType<Task<Result<string[]>>>();
        var actualResult = await actualResultTask;
        actualResult.IsSuccess.Should().BeFalse();
    }
}
