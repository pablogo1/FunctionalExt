namespace FunctionalExt.Tests.Result;

using static Functions;

public partial class CombineTests
{
    public class TwoResultsAsInput
    {
        [Fact]
        public void Should_return_failed_result_if_first_is_failed()
        {
            var error = new GenericError("code", "message");
            var r1 = Fail<string, GenericError>(error);
            var r2 = Success<int, GenericError>(42);

            var combined = Functions.Combine(r1, r2);

            combined.IsSuccess.Should().BeFalse();
            combined.Error.Should().Be(error);
        }

        [Fact]
        public void Should_return_failed_result_if_second_is_failed()
        {
            var error = new GenericError("code", "message");
            var r1 = Success<string, GenericError>("value");
            var r2 = Fail<int, GenericError>(error);

            var combined = Functions.Combine(r1, r2);

            combined.IsSuccess.Should().BeFalse();
            combined.Error.Should().Be(error);
        }

        [Fact]
        public void Should_return_successful_result_if_both_are_successful()
        {
            var r1 = Success<string, GenericError>("value");
            var r2 = Success<int, GenericError>(42);

            var combined = Functions.Combine(r1, r2);

            combined.IsSuccess.Should().BeTrue();
            combined.Value.Should().Be(("value", 42));
        }
    }
}