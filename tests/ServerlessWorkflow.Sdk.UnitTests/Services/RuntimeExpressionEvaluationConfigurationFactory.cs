namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RuntimeExpressionEvaluationConfigurationFactory
{
    internal static RuntimeExpressionEvaluationConfiguration Create() => new()
    {
        Language = RuntimeExpressions.Languages.JQ,
        Mode = RuntimeExpressionEvaluationMode.Strict
    };
}
