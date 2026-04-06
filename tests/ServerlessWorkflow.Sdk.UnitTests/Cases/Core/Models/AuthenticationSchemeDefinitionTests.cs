namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class AuthenticationSchemeDefinitionTests
{

    [Fact]
    public void Basic_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new BasicAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Basic);
    }

    [Fact]
    public void Bearer_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new BearerAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Bearer);
    }

    [Fact]
    public void Certificate_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new CertificateAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Certificate);
    }

    [Fact]
    public void Digest_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new DigestAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Digest);
    }

    [Fact]
    public void Use_Property_Should_Be_Settable()
    {
        //arrange
        var secret = "my-secret";

        //act
        var scheme = new BasicAuthenticationSchemeDefinition { Use = secret };

        //assert
        scheme.Use.Should().Be(secret);
    }

}
