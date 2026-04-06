namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class WaitTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Wait_And_Set_Result_With_Input()
    {
        // Arrange
        var definition = new WaitTaskDefinition { Wait = Duration.FromMilliseconds(10) };
        var input = new JsonObject { ["data"] = "test" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        taskContext.Object.Workflow.Expressions
            .AsIMock()
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new WaitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<WaitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Use_Then_Directive()
    {
        // Arrange
        var definition = new WaitTaskDefinition { Wait = Duration.FromMilliseconds(10), Then = FlowDirective.End };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        taskContext.Object.Workflow.Expressions
            .AsIMock()
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new WaitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<WaitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Be_Cancellable()
    {
        // Arrange
        var definition = new WaitTaskDefinition { Wait = Duration.FromSeconds(5) };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var executor = new WaitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<WaitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));

        // Act & Assert - should not throw; the base class catches OperationCanceledException
        await executor.ExecuteAsync(cts.Token);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var definition = new WaitTaskDefinition { Wait = Duration.FromMilliseconds(10) };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);

        var executor = new WaitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<WaitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

}
