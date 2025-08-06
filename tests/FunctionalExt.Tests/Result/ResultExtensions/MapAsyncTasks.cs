namespace FunctionalExt.Tests.Result;

public class MapAsyncTasks
{
    private static async Task<Result<string, GenericError>> GetStringResultAsync() 
    {
        await Task.Yield();
        return Result<string, GenericError>.CreateSuccess("Lorem ipsum dolor sit amet");
    }

    private static async Task<Result<string, GenericError>> GetStringFailedResultAsync()
    {
        await Task.Yield();
        var error = new GenericError("Code", "Error");
        return Result<string, GenericError>.CreateFail(error);
    }

    private static async Task<string[]> TokenizeAsync(string str)
    {
        await Task.Yield();
        return str.Split(" ");
    }

    [Fact]
    public async Task Should_return_successful_result_of_type_B_given_a_successful_input_result()
    {
        var inputResultTask = GetStringResultAsync();
        Task<Result<string[], GenericError>> actualResultTask = inputResultTask.MapAsync(TokenizeAsync);

        var actualResult = await actualResultTask;
        actualResult.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_return_failed_result_of_type_B_given_a_faulted_input_result()
    {
        var inputResultTask = GetStringFailedResultAsync();
        Task<Result<string[], GenericError>> actualResultTask = inputResultTask.MapAsync(TokenizeAsync);

        var actualResult = await actualResultTask;
        actualResult.IsSuccess.Should().BeFalse();
    }
}
