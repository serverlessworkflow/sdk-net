namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class AsyncApiSubscriptionDefinitionFactory
{
    internal static AsyncApiSubscriptionDefinition Create() => new()
    {
        Filter = ".data.userId != null",
        Consume = AsyncApiSubscriptionLifetimeDefinitionFactory.Create()
    };
}
