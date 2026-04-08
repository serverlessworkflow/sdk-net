namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class CatalogDefinitionFactory
{
    internal static CatalogDefinition Create() => new()
    {
        Endpoint = EndpointDefinitionFactory.CreateSimple()
    };
}
