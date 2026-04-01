namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RetryPolicyDefinitionFactory
{
    internal static RetryPolicyDefinition Create() => new()
    {
        When = "${ .error.status == 503 }",
        ExceptWhen = "${ .error.status == 404 }",
        Limit = RetryPolicyLimitDefinitionFactory.Create(),
        Delay = Duration.FromSeconds(5),
        Backoff = BackoffStrategyDefinitionFactory.CreateExponential(),
        Jitter = JitterDefinitionFactory.Create()
    };
}
