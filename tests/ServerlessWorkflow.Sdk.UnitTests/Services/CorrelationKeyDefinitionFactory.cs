namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class CorrelationKeyDefinitionFactory
{
    internal static CorrelationKeyDefinition Create() => new()
    {
        From = ".correlationId",
        Expect = "${ .correlationId }"
    };
}
