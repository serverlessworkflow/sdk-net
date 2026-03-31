namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ExternalResourceDefinitionFactory
{
    internal static ExternalResourceDefinition Create() => new()
    {
        Name = "test-resource",
        EndpointValue = new Uri("https://api.example.com/resource")
    };

    internal static ExternalResourceDefinition CreateWithEndpointDefinition() => new()
    {
        Name = "test-resource",
        EndpointValue = EndpointDefinitionFactory.CreateSimple()
    };
}
