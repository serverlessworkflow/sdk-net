namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RetryPolicyLimitDefinitionFactory
{
    internal static RetryPolicyLimitDefinition Create() => new()
    {
        Attempt = RetryAttemptLimitDefinitionFactory.Create(),
        Duration = Duration.FromMinutes(30)
    };
}
