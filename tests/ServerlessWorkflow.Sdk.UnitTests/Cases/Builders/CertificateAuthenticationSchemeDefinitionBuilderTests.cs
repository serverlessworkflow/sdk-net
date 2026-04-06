namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class CertificateAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Secret_Reference_And_Scheme()
    {
        //arrange
        var secret = "my-cert-secret";
        var builder = new CertificateAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);

        //act
        var scheme = builder.Build();

        //assert
        scheme.Use.Should().Be(secret);
        scheme.Scheme.Should().Be(AuthenticationScheme.Certificate);
    }

}
