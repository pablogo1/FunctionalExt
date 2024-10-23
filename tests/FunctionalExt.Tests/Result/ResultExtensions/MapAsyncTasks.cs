namespace FunctionalExt.Tests.Result;

public class MapAsyncTasks
{
    private static async Task<Result<string>> GetStringResultAsync() 
    {
        await Task.Delay(10);
        return Result<string>.Create("Lorem ipsum dolor sit amet");
    }

    private static async Task<Result<string>> GetStringFailedResultAsync()
    {
        await Task.Delay(10);
        Error error = new GenericError("Code", "Error");
        return Result<string>.Create(error);
    }

    private static async Task<string[]> TokenizeAsync(string str)
    {
        await Task.Delay(10);
        return str.Split(" ");
    }

    [Fact]
    public async Task Should_return_successful_result_of_type_B_given_a_successful_input_result()
    {
        var inputResultTask = GetStringResultAsync();
        Task<Result<string[]>> actualResultTask = inputResultTask.MapAsync(TokenizeAsync);

        var actualResult = await actualResultTask;
        actualResult.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_return_failed_result_of_type_B_given_a_faulted_input_result()
    {
        var inputResultTask = GetStringFailedResultAsync();
        Task<Result<string[]>> actualResultTask = inputResultTask.MapAsync(TokenizeAsync);

        var actualResult = await actualResultTask;
        actualResult.IsSuccess.Should().BeFalse();
    }
}
