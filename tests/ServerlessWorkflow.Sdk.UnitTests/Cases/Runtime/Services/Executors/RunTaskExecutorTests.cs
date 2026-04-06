namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class RunTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Container_Should_Create_And_Start_Container()
    {
        // Arrange
        var containerDef = new ContainerProcessDefinition { Image = "alpine:latest" };
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Container = containerDef }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var stdoutReader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("hello world")));
        var container = new Mock<IContainer>();
        container.Setup(c => c.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        container.Setup(c => c.WaitForExitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        container.Setup(c => c.StandardOutput).Returns(stdoutReader);
        container.Setup(c => c.StandardError).Returns((StreamReader?)null);

        var containerRuntime = new Mock<IContainerRuntime>();
        containerRuntime.Setup(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(container.Object);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new RunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        containerRuntime.Verify(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()), Times.Once);
        container.Verify(c => c.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Execute_Container_Should_Set_Result_With_Output()
    {
        // Arrange
        var containerDef = new ContainerProcessDefinition { Image = "alpine:latest" };
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Container = containerDef }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var stdoutReader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("container output")));
        var container = new Mock<IContainer>();
        container.Setup(c => c.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        container.Setup(c => c.WaitForExitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        container.Setup(c => c.StandardOutput).Returns(stdoutReader);
        container.Setup(c => c.StandardError).Returns((StreamReader?)null);

        var containerRuntime = new Mock<IContainerRuntime>();
        containerRuntime.Setup(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(container.Object);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new RunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Container_Should_Return_Immediately_When_Await_Is_False()
    {
        // Arrange
        var containerDef = new ContainerProcessDefinition { Image = "alpine:latest" };
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Container = containerDef, Await = false }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var container = new Mock<IContainer>();
        container.Setup(c => c.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var containerRuntime = new Mock<IContainerRuntime>();
        containerRuntime.Setup(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(container.Object);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new RunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should NOT wait for exit
        container.Verify(c => c.WaitForExitAsync(It.IsAny<CancellationToken>()), Times.Never);
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Container_Should_Set_Error_On_Exception()
    {
        // Arrange
        var containerDef = new ContainerProcessDefinition { Image = "alpine:latest" };
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Container = containerDef }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var container = new Mock<IContainer>();
        container.Setup(c => c.StartAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new InvalidOperationException("Container failed to start"));
        container.Setup(c => c.StandardError).Returns((StreamReader?)null);

        var containerRuntime = new Mock<IContainerRuntime>();
        containerRuntime.Setup(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(container.Object);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new RunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetErrorAsync(It.IsAny<IRuntimeError>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Throw_When_No_Process_Type_Configured()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition()
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var containerRuntime = new Mock<IContainerRuntime>();

        var executor = new RunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should set a runtime error because neither container nor shell is configured
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetErrorAsync(It.IsAny<IRuntimeError>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var containerDef = new ContainerProcessDefinition { Image = "alpine:latest" };
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Container = containerDef }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var containerRuntime = new Mock<IContainerRuntime>();

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);

        var executor = new RunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        containerRuntime.Verify(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()), Times.Never);
    }

}
