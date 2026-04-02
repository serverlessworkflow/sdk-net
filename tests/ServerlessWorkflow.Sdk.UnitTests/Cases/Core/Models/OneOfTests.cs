namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class OneOfTests
{
    [Fact]
    public void TryGetAsT1_Should_Return_True_When_T1_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = "hello";
        //act
        var result = oneOf.TryGetAsT1(out var value);
        //assert
        result.Should().BeTrue();
        value.Should().Be("hello");
    }

    [Fact]
    public void TryGetAsT1_Should_Return_False_When_T2_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = 42;
        //act
        var result = oneOf.TryGetAsT1(out _);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void TryGetAsT2_Should_Return_True_When_T2_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = 42;
        //act
        var result = oneOf.TryGetAsT2(out var value);
        //assert
        result.Should().BeTrue();
        value.Should().Be(42);
    }

    [Fact]
    public void TryGetAsT2_Should_Return_False_When_T1_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = "hello";
        //act
        var result = oneOf.TryGetAsT2(out _);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Match_Should_Invoke_F1_When_T1_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = "hello";
        //act
        var result = oneOf.Match(s => s.Length, i => i);
        //assert
        result.Should().Be(5);
    }

    [Fact]
    public void Match_Should_Invoke_F2_When_T2_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = 42;
        //act
        var result = oneOf.Match(s => s.Length, i => i);
        //assert
        result.Should().Be(42);
    }

    [Fact]
    public void Switch_Should_Invoke_A1_When_T1_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = "hello";
        string? captured = null;
        //act
        oneOf.Switch(s => captured = s, _ => { });
        //assert
        captured.Should().Be("hello");
    }

    [Fact]
    public void Switch_Should_Invoke_A2_When_T2_Is_Set()
    {
        //arrange
        OneOf<string, int> oneOf = 42;
        int captured = 0;
        //act
        oneOf.Switch(_ => { }, i => captured = i);
        //assert
        captured.Should().Be(42);
    }

    [Fact]
    public void Implicit_Conversion_From_T1_Should_Work()
    {
        //arrange & act
        OneOf<string, int> oneOf = "test";
        //assert
        oneOf.TryGetAsT1(out var value).Should().BeTrue();
        value.Should().Be("test");
    }

    [Fact]
    public void Implicit_Conversion_From_T2_Should_Work()
    {
        //arrange & act
        OneOf<string, int> oneOf = 99;
        //assert
        oneOf.TryGetAsT2(out var value).Should().BeTrue();
        value.Should().Be(99);
    }
}
