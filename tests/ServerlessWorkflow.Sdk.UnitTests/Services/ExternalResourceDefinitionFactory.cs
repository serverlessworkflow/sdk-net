namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ExternalResourceDefinitionFactory
{
    internal static ExternalResourceDefinition Create() => new()
    {
        Name = "test-resource",
        Endpoint = new Uri("https://api.example.com/resource")
    };

    internal static ExternalResourceDefinition CreateWithEndpointDefinition() => new()
    {
        Name = "test-resource",
        Endpoint = EndpointDefinitionFactory.CreateSimple()
    };
}
