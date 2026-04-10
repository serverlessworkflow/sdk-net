namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class WorkflowRunTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Workflow_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Workflow = new WorkflowProcessDefinition { Namespace = "test", Name = "sub-workflow" } }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new WorkflowRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<WorkflowRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_Workflow_Should_Set_Error_As_Not_Supported()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Workflow = new WorkflowProcessDefinition { Namespace = "test", Name = "sub-workflow" } }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new WorkflowRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<WorkflowRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should set error because workflow process is not yet supported
        Mock.Get(taskContext.Object.State).Verify(
            i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

}
