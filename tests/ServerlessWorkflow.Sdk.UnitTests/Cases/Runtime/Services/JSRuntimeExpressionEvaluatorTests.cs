namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public sealed class JSRuntimeExpressionEvaluatorTests
    : IRuntimeExpressionEvaluatorTests
{

    protected override IRuntimeExpressionEvaluator ExpressionEvaluator { get; } = new JSRuntimeExpressionEvaluator();

    [Fact]
    public override async Task Evaluate_Expression_Should_Work()
    {
        // arrange
        var expression = "1 + 2";

        // act
        var result = await ExpressionEvaluator.EvaluateAsync(expression, [], null, TestContext.Current.CancellationToken);

        // assert
        result.Should().NotBeNull();
        result.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

    [Fact]
    public override async Task Evaluate_Expression_Against_Input_Should_Work()
    {
        // arrange
        var expression = "$.value + 2";
        var input = new JsonObject
        {
            ["value"] = 1
        };

        // act
        var result = await ExpressionEvaluator.EvaluateAsync(expression, input, null, TestContext.Current.CancellationToken);

        // assert
        result.Should().NotBeNull();
        result.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

    [Fact]
    public override async Task Evaluate_Expression_Against_Arguments_Should_Work()
    {
        // arrange
        var arguments = new JsonObject()
        {
            ["ARG1"] = 2
        };

        var input = new JsonObject
        {
            ["value"] = 1
        };

        var expression = "$.value + ARG1";

        // act
        var result = await ExpressionEvaluator.EvaluateAsync(expression, input, arguments, TestContext.Current.CancellationToken);

        // assert
        result.Should().NotBeNull();
        result.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

    [Fact]
    public override async Task Evaluate_JsonObject_Should_Work()
    {
        // arrange
        var propertyName = "additionResult";

        var arguments = new JsonObject()
        {
            ["ARG1"] = 2
        };

        var input = new JsonObject
        {
            ["value"] = 1
        };

        var value = new JsonObject()
        {
            [propertyName] = "${ $.value + ARG1 }"
        };

        // act
        var result = await ExpressionEvaluator.EvaluateAsync(value, input, arguments, TestContext.Current.CancellationToken);

        // assert
        result.Should().NotBeNull();
        result.AsObject().TryGetPropertyValue(propertyName, out var propertyValue).Should().BeTrue();
        propertyValue.Should().NotBeNull();
        propertyValue.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

}
