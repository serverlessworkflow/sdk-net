namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationRequestDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Request_With_Encoding()
    {
        //arrange
        var encoding = "application/x-www-form-urlencoded";

        //act
        var request = new OAuth2AuthenticationRequestDefinitionBuilder()
            .WithEncoding(encoding)
            .Build();

        //assert
        request.Encoding.Should().Be(encoding);
    }

}
