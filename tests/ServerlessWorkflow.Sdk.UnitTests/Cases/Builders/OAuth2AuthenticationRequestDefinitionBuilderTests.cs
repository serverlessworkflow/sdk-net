namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationRequestDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Request_With_Encoding()
    {
        var encoding = "application/x-www-form-urlencoded";
        var request = new OAuth2AuthenticationRequestDefinitionBuilder()
            .WithEncoding(encoding)
            .Build();
        request.Encoding.Should().Be(encoding);
    }

}
