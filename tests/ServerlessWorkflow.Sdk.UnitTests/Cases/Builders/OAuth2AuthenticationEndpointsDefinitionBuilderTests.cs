namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationEndpointsDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Endpoints_With_Custom_Uris()
    {
        var tokenUri = new Uri("/custom/token", UriKind.Relative);
        var revocationUri = new Uri("/custom/revoke", UriKind.Relative);
        var introspectionUri = new Uri("/custom/introspect", UriKind.Relative);
        var endpoints = new OAuth2AuthenticationEndpointsDefinitionBuilder()
            .WithTokenEndpoint(tokenUri)
            .WithRevocationEndpoint(revocationUri)
            .WithIntrospectionEndpoint(introspectionUri)
            .Build();
        endpoints.Token.Should().Be(tokenUri);
        endpoints.Revocation.Should().Be(revocationUri);
        endpoints.Introspection.Should().Be(introspectionUri);
    }

    [Fact]
    public void Build_Should_Use_Default_Endpoints_When_Not_Configured()
    {
        var endpoints = new OAuth2AuthenticationEndpointsDefinitionBuilder().Build();
        endpoints.Token.Should().NotBeNull();
        endpoints.Revocation.Should().NotBeNull();
        endpoints.Introspection.Should().NotBeNull();
    }

}
