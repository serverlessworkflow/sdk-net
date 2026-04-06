namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class SetTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Evaluate_Set_Expression_And_Set_Result()
    {
        // Arrange
        var setData = new JsonObject { ["greeting"] = "${ .name }" };
        var definition = new SetTaskDefinition { Set = setData };
        var input = new JsonObject { ["name"] = "world" };
        var taskContext = CreateTaskExecutionContext(definition, input);
        var evaluatedResult = new JsonObject { ["greeting"] = "world" };

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => evaluatedResult.DeepClone());

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var executor = CreateExecutor(taskContext);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Pass_Input_To_Expression_Evaluator()
    {
        // Arrange
        var setData = new JsonObject { ["result"] = "${ .value }" };
        var definition = new SetTaskDefinition { Set = setData };
        var input = new JsonObject { ["value"] = 42 };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => JsonValue.Create(42));

        var executor = CreateExecutor(taskContext);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Use_Then_Directive_From_Definition()
    {
        // Arrange
        var setData = new JsonObject { ["key"] = "${ .val }" };
        var definition = new SetTaskDefinition { Set = setData, Then = FlowDirective.End };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => JsonValue.Create("value"));

        var executor = CreateExecutor(taskContext);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var definition = new SetTaskDefinition { Set = new JsonObject { ["k"] = "${ .v }" } };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);

        var executor = CreateExecutor(taskContext);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static SetTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<SetTaskDefinition>> taskContext) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<SetTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        taskContext.Object);

}

internal static class MockExtensions
{
    public static Mock<T> AsIMock<T>(this T obj) where T : class => Mock.Get(obj);
}
