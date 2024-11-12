namespace FunctionalExt.Tests.Result;

public class IfFailTests
{
    public class GivenFaultedResult 
    {
        private readonly Result<string> _failedResult = Result<string>.Create(new GenericError("code", "message"));
        private const string DefaultValue = "my default value";

        [Fact]
        public void Should_return_given_default_value()
        {
            string actual = _failedResult.IfFail(DefaultValue);

            actual.Should().Be(DefaultValue);
        }

        [Fact]
        public void Should_return_default_value_using_given_function()
        {
            string actual = _failedResult.IfFail(() => DefaultValue);

            actual.Should().Be(DefaultValue);
        }
        
        [Fact]
        public void Should_return_value_given_by_failFn()
        {
            string actual = _failedResult.IfFail(error => DefaultValue);

            actual.Should().Be(DefaultValue);
        }

        [Fact]
        public void Should_execute_action_and_return_unit()
        {
            bool called = false;
            void Callback(Error error)
            {
                called = true;
            }
            
            var actual = _failedResult.IfFail(Callback);

            called.Should().BeTrue();
        }
    }

    public class GivenSuccessResult
    {
        [Fact]
        public void Should_return_wrapped_value_when_input_result_is_success()
        {
            const string defaultValue = "default";
            var successResult = Result<string>.Create("This is a test");
            
            string actual1 = successResult.IfFail(defaultValue);
            string actual2 = successResult.IfFail(() => defaultValue);

            actual1.Should().NotBe(defaultValue);
            actual2.Should().NotBe(defaultValue);
        }
    }
}
