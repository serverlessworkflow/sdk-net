namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class AsyncApiSubscriptionLifetimeDefinitionFactory
{
    internal static AsyncApiSubscriptionLifetimeDefinition Create() => new()
    {
        Amount = 10,
        While = ".data.active == true",
        Until = ".data.done == true",
        For = DurationFactory.Create()
    };
}
