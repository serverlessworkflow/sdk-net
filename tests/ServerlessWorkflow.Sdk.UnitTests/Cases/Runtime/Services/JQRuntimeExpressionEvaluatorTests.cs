using Microsoft.Extensions.Logging.Abstractions;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public sealed class JQRuntimeExpressionEvaluatorTests 
    : IRuntimeExpressionEvaluatorTests
{

    protected override IRuntimeExpressionEvaluator ExpressionEvaluator { get; } = new JQRuntimeExpressionEvaluator(new NullLogger<JQRuntimeExpressionEvaluator>());

}