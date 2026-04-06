namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class CertificateAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Secret_Reference_And_Scheme()
    {
        var secret = "my-cert-secret";
        var builder = new CertificateAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);
        var scheme = builder.Build();
        scheme.Use.Should().Be(secret);
        scheme.Scheme.Should().Be(AuthenticationScheme.Certificate);
    }

}
