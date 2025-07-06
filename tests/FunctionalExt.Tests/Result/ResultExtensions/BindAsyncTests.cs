namespace FunctionalExt.Tests.Result;

public class BindAsyncTests
{
    private static async Task<Result<string, GenericError>> GetStringResultAsync(string contents)
    {
        await Task.Yield();
        return Result<string, GenericError>.CreateSuccess(contents);
    }

    private static async Task<Result<string, GenericError>> GetStringFailedResultAsync()
    {
        await Task.Yield();
        var error = new GenericError("Code", "Error");
        return Result<string, GenericError>.CreateFail(error);
    }   

    public class TaskBasedResultAsInput_And_NotAsyncBindFn
    {
        private static Result<string[], GenericError> Tokenize(string str) =>
            string.IsNullOrWhiteSpace(str)
                ? Result<string[], GenericError>.CreateFail(new GenericError("EmptyError", "Empty string"))
                : Result<string[], GenericError>.CreateSuccess(str.Split(" "));

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

        [Fact]
        public async Task Should_return_failed_result_with_error_from_input_result()
        {
            var failedResultTask = GetStringFailedResultAsync();

            var tokenizedResult = await failedResultTask.BindAsync(Tokenize);

            tokenizedResult.IsSuccess.Should().BeFalse();
            tokenizedResult.Error!.Code.Should().Be("Code");
            tokenizedResult.Error!.Message.Should().Be("Error");
        }
    }

    public class TaskBasedResultAsInput_And_AsyncBindFn
    {
        private static async Task<Result<string[], GenericError>> Tokenize(string str)
        {
            await Task.Yield();
            return string.IsNullOrWhiteSpace(str)
                ? Result<string[], GenericError>.CreateFail(new GenericError("EmptyError", "Empty string"))
                : Result<string[], GenericError>.CreateSuccess(str.Split(" "));
        }

        [Fact]
        public async Task Should_return_result_of_type_B_given_by_the_execution_of_bindAsyncFn_on_a_successful_result()
        {
            var nonEmptyResultTask = GetStringResultAsync("Lorem ipsum dolor sit amet");
            var emptyResultTask = GetStringResultAsync("");

            var nonEmptyTokenResult = await nonEmptyResultTask.BindAsync(Tokenize);
            var emptyTokenResult = await emptyResultTask.BindAsync(Tokenize);

            nonEmptyTokenResult.IsSuccess.Should().BeTrue();
            emptyTokenResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task Should_return_failed_result_with_error_from_input_result()
        {
            var failedResultTask = GetStringFailedResultAsync();

            var tokenizedResult = await failedResultTask.BindAsync(Tokenize);

            tokenizedResult.IsSuccess.Should().BeFalse();
            tokenizedResult.Error!.Code.Should().Be("Code");
            tokenizedResult.Error!.Message.Should().Be("Error");
        }
    }
}
