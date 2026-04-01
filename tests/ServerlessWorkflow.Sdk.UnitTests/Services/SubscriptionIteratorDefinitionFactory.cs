namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class SubscriptionIteratorDefinitionFactory
{
    internal static SubscriptionIteratorDefinition Create() => new()
    {
        Item = "event",
        At = "eventIndex",
        Output = OutputDataModelDefinitionFactory.Create()
    };
}
