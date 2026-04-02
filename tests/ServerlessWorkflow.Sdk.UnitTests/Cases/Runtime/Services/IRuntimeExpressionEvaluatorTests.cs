namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public abstract class IRuntimeExpressionEvaluatorTests
{

    protected abstract IRuntimeExpressionEvaluator ExpressionEvaluator { get; }

    [Fact]
    public abstract Task Evaluate_Expression_Should_Work();

    [Fact]
    public abstract Task Evaluate_Expression_Against_Input_Should_Work();

    [Fact]
    public abstract Task Evaluate_Expression_Against_Arguments_Should_Work();

    [Fact]
    public abstract Task Evaluate_JsonObject_Should_Work();

}
