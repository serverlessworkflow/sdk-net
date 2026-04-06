namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationClientDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Client_With_All_Properties()
    {
        var clientId = "my-client-id";
        var clientSecret = "my-client-secret";
        var assertion = "jwt-assertion";
        var authMethod = "client_secret_post";
        var client = new OAuth2AuthenticationClientDefinitionBuilder()
            .WithId(clientId)
            .WithSecret(clientSecret)
            .WithAssertion(assertion)
            .WithAuthenticationMethod(authMethod)
            .Build();
        client.Id.Should().Be(clientId);
        client.Secret.Should().Be(clientSecret);
        client.Assertion.Should().Be(assertion);
        client.Authentication.Should().Be(authMethod);
    }

    [Fact]
    public void Build_Should_Create_Client_With_Minimal_Properties()
    {
        var clientId = "minimal-client";
        var client = new OAuth2AuthenticationClientDefinitionBuilder()
            .WithId(clientId)
            .Build();
        client.Id.Should().Be(clientId);
        client.Secret.Should().BeNull();
    }

}
