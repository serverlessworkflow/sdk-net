namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class AuthenticationResultTests
{

    [Fact]
    public void Should_Set_Scheme_And_Value()
    {
        //arrange
        var scheme = "Bearer";
        var value = "eyJhbGciOiJSUzI1NiJ9...";

        //act
        var result = new AuthenticationResult(scheme, value);

        //assert
        result.Scheme.Should().Be(scheme);
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Should_Implement_IAuthenticationResult()
    {
        //arrange
        var scheme = "Basic";
        var value = "dXNlcjpwYXNz";

        //act
        IAuthenticationResult result = new AuthenticationResult(scheme, value);

        //assert
        result.Scheme.Should().Be(scheme);
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Should_Support_Record_Equality()
    {
        //arrange
        var scheme = "Bearer";
        var value = "token123";
        var result1 = new AuthenticationResult(scheme, value);
        var result2 = new AuthenticationResult(scheme, value);

        //act & assert
        result1.Should().Be(result2);
    }

    [Fact]
    public void Should_Support_Record_With_Expression()
    {
        //arrange
        var originalScheme = "Bearer";
        var originalValue = "old-token";
        var newValue = "new-token";
        var original = new AuthenticationResult(originalScheme, originalValue);

        //act
        var updated = original with { Value = newValue };

        //assert
        updated.Scheme.Should().Be(originalScheme);
        updated.Value.Should().Be(newValue);
        original.Value.Should().Be(originalValue);
    }

}
