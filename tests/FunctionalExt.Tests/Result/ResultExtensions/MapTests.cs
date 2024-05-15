﻿using System.Security.AccessControl;

namespace FunctionalExt.Tests.Result;

public class MapTests
{
    [Fact]
    public void Should_return_successful_result_of_type_B_given_a_successful_input_result()
    {
        var inputResult = Result<string, Error>.Create("this ia a test");

        var actualResult = inputResult.Map(str => str.Split(" "));

        actualResult.IsSuccess.Should().BeTrue();
        actualResult.Value.Should().BeOfType<string[]>();
    }

    [Fact]
    public void Should_return_failed_result_of_type_B_given_failed_input_result()
    {
        Error error = new ("Code", "Message");
        var inputResult = Result<string, Error>.Create(error);

        var actualResult = inputResult.Map(str => str.Split(" "));

        actualResult.IsSuccess.Should().BeFalse();
        actualResult.Error.Should().Be(error);
    }

    [Fact]
    public void Should_throw_ResultUndefinedException_given_a_undefined_result_as_input()
    {
        var result = new Result<string, Error>();

        Assert.Throws<ResultUndefinedException>(() => _ = result.Map(str => str.Split(" ")));
    }
}