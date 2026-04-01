namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class BackoffStrategyDefinitionFactory
{
    internal static BackoffStrategyDefinition CreateConstant() => new()
    {
        Constant = ConstantBackoffDefinitionFactory.Create()
    };

    internal static BackoffStrategyDefinition CreateExponential() => new()
    {
        Exponential = ExponentialBackoffDefinitionFactory.Create()
    };

    internal static BackoffStrategyDefinition CreateLinear() => new()
    {
        Linear = LinearBackoffDefinitionFactory.Create()
    };
}
