namespace FunctionalExt.Tests.Result;

public class MatchAsyncTests
{
    public class TaskBasedResultAsInput_And_AsyncMatchFn
    {
        private static async Task<string[]> ExecuteTest(Task<Result<string, GenericError>> resultTask) => await resultTask.MatchAsync(
            str => Task.FromResult(str.Split(" ")),
            err => Task.FromResult(Array.Empty<string>())
        );

        // Add tests for MatchAsync here
        [Fact]
        public async Task Should_return_succFn_value_when_result_is_success()
        {
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateSuccess("Lorem ipsum dolor sit amet"));

            var output = await ExecuteTest(resultTask);

            output.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Should_return_errFn_value_when_result_is_faulted()
        {
            var error = new GenericError("Error", "Test Error");
            var resultTask = Task.FromResult(Result<string, GenericError>.CreateFail(error));

            var output = await ExecuteTest(resultTask);

            output.Should().BeEmpty();
        }

        [Fact]
        public async Task Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
        {
            var resultTask = Task.FromResult(new Result<string, GenericError>());

            await Assert.ThrowsAsync<ResultUndefinedException>(() => ExecuteTest(resultTask));
        }
    }

    public class ResultAsInput_And_AsyncMatchFn
    {
        private static async Task<string[]> ExecuteTest(Result<string, GenericError> resultTask) => await resultTask.MatchAsync(
            str => Task.FromResult(str.Split(" ")),
            err => Task.FromResult(Array.Empty<string>())
        );


        [Fact]
        public async Task Should_return_succFn_value_when_result_is_success()
        {
            var result = Result<string, GenericError>.CreateSuccess("Lorem ipsum dolor sit amet");

            var output = await ExecuteTest(result);

            output.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Should_return_errFn_value_when_result_is_faulted()
        {
            var error = new GenericError("Error", "Test Error");
            var result = Result<string, GenericError>.CreateFail(error);

            var output = await ExecuteTest(result);

            output.Should().BeEmpty();
        }

        [Fact]
        public async Task Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
        {
            var result = new Result<string, GenericError>();

            await Assert.ThrowsAsync<ResultUndefinedException>(() => ExecuteTest(result));
        }
    }
}

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
