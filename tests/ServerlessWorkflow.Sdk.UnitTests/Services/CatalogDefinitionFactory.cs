namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class CatalogDefinitionFactory
{
    internal static CatalogDefinition Create() => new()
    {
        EndpointValue = EndpointDefinitionFactory.CreateSimple()
    };
}
