namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RaiseTaskDefinitionFactory
{
    internal static RaiseTaskDefinition Create() => new()
    {
        Raise = RaiseErrorDefinitionFactory.Create()
    };
}
