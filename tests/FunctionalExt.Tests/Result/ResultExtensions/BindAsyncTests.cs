namespace FunctionalExt.Tests.Result;

public class BindAsyncTests
{
    private static async Task<Result<string, GenericError>> GetStringResultAsync(string contents) 
    {
        await Task.Delay(10);
        return Result<string, GenericError>.CreateSuccess(contents);
    }

    private static async Task<Result<string, GenericError>> GetStringFailedResultAsync()
    {
        await Task.Delay(10);
        var error = new GenericError("Code", "Error");
        return Result<string, GenericError>.CreateFail(error);
    }

    private static Result<string[], GenericError> Tokenize(string str) => 
        string.IsNullOrWhiteSpace(str)
            ? Result<string[], GenericError>.CreateFail(new GenericError("EmptyError", "Empty string"))
            : Result<string[], GenericError>.CreateSuccess(str.Split(" "));

    public class TaskBasedResultAsInput_And_NotAsyncBindFn
    {
        [Fact]
        public async Task Should_return_result_of_type_B_given_by_the_execution_of_bindFn_on_a_successful_result()
        {
            var nonEmptyResultTask = GetStringResultAsync("Lorem ipsum dolor sit amet");
            var emptyResultTask = GetStringResultAsync("");

            var nonEmptyTokenResult = await nonEmptyResultTask.BindAsync(Tokenize);
            var emptyTokenResult = await emptyResultTask.BindAsync(Tokenize);

            nonEmptyTokenResult.IsSuccess.Should().BeTrue();
            emptyTokenResult.IsSuccess.Should().BeFalse();
        }
    }
}
