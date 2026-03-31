namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EndpointDefinitionFactory
{
    internal static EndpointDefinition Create() => new()
    {
        Uri = new Uri("https://api.example.com/v1"),
        Authentication = AuthenticationPolicyDefinitionFactory.CreateBasic()
    };

    internal static EndpointDefinition CreateSimple() => new()
    {
        Uri = new Uri("https://api.example.com/v1")
    };
}
