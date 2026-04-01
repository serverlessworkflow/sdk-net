namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RetryAttemptLimitDefinitionFactory
{
    internal static RetryAttemptLimitDefinition Create() => new()
    {
        Count = 3,
        Duration = Duration.FromMinutes(5)
    };
}
