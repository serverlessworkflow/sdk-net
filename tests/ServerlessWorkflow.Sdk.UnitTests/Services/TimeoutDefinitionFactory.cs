namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class TimeoutDefinitionFactory
{
    internal static TimeoutDefinition Create() => new()
    {
        After = DurationFactory.Create()
    };

    internal static TimeoutDefinition CreateWithExpression() => new()
    {
        After = ".timeout"
    };
}
