namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationEndpointsDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Endpoints_With_Custom_Uris()
    {
        //arrange
        var tokenUri = new Uri("/custom/token", UriKind.Relative);
        var revocationUri = new Uri("/custom/revoke", UriKind.Relative);
        var introspectionUri = new Uri("/custom/introspect", UriKind.Relative);

        //act
        var endpoints = new OAuth2AuthenticationEndpointsDefinitionBuilder()
            .WithTokenEndpoint(tokenUri)
            .WithRevocationEndpoint(revocationUri)
            .WithIntrospectionEndpoint(introspectionUri)
            .Build();

        //assert
        endpoints.Token.Should().Be(tokenUri);
        endpoints.Revocation.Should().Be(revocationUri);
        endpoints.Introspection.Should().Be(introspectionUri);
    }

    [Fact]
    public void Build_Should_Use_Default_Endpoints_When_Not_Configured()
    {
        //arrange
        var builder = new OAuth2AuthenticationEndpointsDefinitionBuilder();

        //act
        var endpoints = builder.Build();

        //assert
        endpoints.Token.Should().NotBeNull();
        endpoints.Revocation.Should().NotBeNull();
        endpoints.Introspection.Should().NotBeNull();
    }

}
