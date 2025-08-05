namespace FunctionalExt.Tests.Result;

using static Functions;

public partial class CombineTests
{
    public class FourResultsAsInput
    {
        [Fact]
        public void Should_return_failed_result_if_first_is_failed()
        {
            var error = new GenericError("code", "message");
            var r1 = Fail<string, GenericError>(error);
            var r2 = Success<int, GenericError>(42);
            var r3 = Success<bool, GenericError>(true);
            var r4 = Success<double, GenericError>(3.14);

            var combined = Functions.Combine(r1, r2, r3, r4);

            combined.IsSuccess.Should().BeFalse();
            combined.Error.Should().Be(error);
        }

        [Fact]
        public void Should_return_failed_result_if_second_is_failed()
        {
            var error = new GenericError("code", "message");
            var r1 = Success<string, GenericError>("value");
            var r2 = Fail<int, GenericError>(error);
            var r3 = Success<bool, GenericError>(true);
            var r4 = Success<double, GenericError>(3.14);

            var combined = Functions.Combine(r1, r2, r3, r4);

            combined.IsSuccess.Should().BeFalse();
            combined.Error.Should().Be(error);
        }

        [Fact]
        public void Should_return_failed_result_if_third_is_failed()
        {
            var error = new GenericError("code", "message");
            var r1 = Success<string, GenericError>("value");
            var r2 = Success<int, GenericError>(42);
            var r3 = Fail<bool, GenericError>(error);
            var r4 = Success<double, GenericError>(3.14);

            var combined = Functions.Combine(r1, r2, r3, r4);

            combined.IsSuccess.Should().BeFalse();
            combined.Error.Should().Be(error);
        }

        [Fact]
        public void Should_return_failed_result_if_fourth_is_failed()
        {
            var error = new GenericError("code", "message");
            var r1 = Success<string, GenericError>("value");
            var r2 = Success<int, GenericError>(42);
            var r3 = Success<bool, GenericError>(true);
            var r4 = Fail<double, GenericError>(error);

            var combined = Functions.Combine(r1, r2, r3, r4);

            combined.IsSuccess.Should().BeFalse();
            combined.Error.Should().Be(error);
        }

        [Fact]
        public void Should_return_successful_result_if_all_are_successful()
        {
            var r1 = Success<string, GenericError>("value");
            var r2 = Success<int, GenericError>(42);
            var r3 = Success<bool, GenericError>(true);
            var r4 = Success<double, GenericError>(3.14);

            var combined = Functions.Combine(r1, r2, r3, r4);

            combined.IsSuccess.Should().BeTrue();
            combined.Value.Should().Be(("value", 42, true, 3.14));
        }
    }
}