namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ErrorCatcherDefinitionFactory
{
    internal static ErrorCatcherDefinition Create() => new()
    {
        Errors = ErrorFilterDefinitionFactory.Create(),
        As = "error",
        When = "${ .error.status == 503 }",
        ExceptWhen = "${ .error.status == 404 }",
        RetryValue = RetryPolicyDefinitionFactory.Create()
    };
}
