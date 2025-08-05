namespace FunctionalExt.Tests.Result;

public class IfFailAsyncTests
{
    public class ResultAsInput
    {
        [Fact]
        public async Task Should_return_value_when_result_is_success()
        {
            var result = Result<string, GenericError>.CreateSuccess("Success");

            var output = await result.IfFailAsync(() => Task.FromResult("Default"));

            output.Should().Be("Success");
        }

        [Fact]
        public async Task Should_return_default_value_when_result_is_faulted()
        {
            var error = new GenericError("Error", "Test Error");
            var result = Result<string, GenericError>.CreateFail(error);

            var output = await result.IfFailAsync(() => Task.FromResult("Default"));

            output.Should().Be("Default");
        }

        [Fact]
        public async Task Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
        {
            var result = new Result<string, GenericError>();

            await Assert.ThrowsAsync<ResultUndefinedException>(() => result.IfFailAsync(() => Task.FromResult("Default")));
        }
    }

    public class TaskBasedResultAsInput_WithAsyncDefaultValueFn
    {
        [Fact]
        public async Task Should_return_value_when_result_is_success()
        {
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateSuccess("Success"));

            var output = await resultTask.IfFailAsync(() => Task.FromResult("Default"));

            output.Should().Be("Success");
        }

        [Fact]
        public async Task Should_return_default_value_when_result_is_faulted()
        {
            var error = new GenericError("Error", "Test Error");
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateFail(error));

            var output = await resultTask.IfFailAsync(() => Task.FromResult("Default"));

            output.Should().Be("Default");
        }

        [Fact]
        public async Task Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
        {
            var resultTask = Task.FromResult(new Result<string, GenericError>());

            await Assert.ThrowsAsync<ResultUndefinedException>(() => resultTask.IfFailAsync(() => Task.FromResult("Default")));
        }
    }

    public class TaskBasedResultAsInput_WithSyncDefaultValueFn
    {
        [Fact]
        public async Task Should_return_value_when_result_is_success()
        {
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateSuccess("Success"));

            var output = await resultTask.IfFailAsync(() => "Default");

            output.Should().Be("Success");
        }

        [Fact]
        public async Task Should_return_default_value_when_result_is_faulted()
        {
            var error = new GenericError("Error", "Test Error");
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateFail(error));

            var output = await resultTask.IfFailAsync(() => "Default");

            output.Should().Be("Default");
        }

        [Fact]
        public async Task Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
        {
            var resultTask = Task.FromResult(new Result<string, GenericError>());

            await Assert.ThrowsAsync<ResultUndefinedException>(() => resultTask.IfFailAsync(() => "Default"));
        }
    }

    public class TaskBasedResultAsInput_WithConstantDefaultValue
    {
        [Fact]
        public async Task Should_return_value_when_result_is_success()
        {
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateSuccess("Success"));

            var output = await resultTask.IfFailAsync("Default");

            output.Should().Be("Success");
        }

        [Fact]
        public async Task Should_return_default_value_when_result_is_faulted()
        {
            var error = new GenericError("Error", "Test Error");
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateFail(error));

            var output = await resultTask.IfFailAsync("Default");

            output.Should().Be("Default");
        }

        [Fact]
        public async Task Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
        {
            var resultTask = Task.FromResult(new Result<string, GenericError>());

            await Assert.ThrowsAsync<ResultUndefinedException>(() => resultTask.IfFailAsync("Default"));
        }
    }
}
