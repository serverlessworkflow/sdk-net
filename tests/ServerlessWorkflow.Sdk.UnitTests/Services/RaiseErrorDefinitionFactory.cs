namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RaiseErrorDefinitionFactory
{
    internal static RaiseErrorDefinition Create() => new()
    {
        Error = ErrorDefinitionFactory.Create()
    };
}
