namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class ContainerRunTaskExecutorTests
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

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

        var executor = new ContainerRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ContainerRunTaskExecutor>>(),
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

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

        var executor = new ContainerRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ContainerRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(
            c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var container = new Mock<IContainer>();
        container.Setup(c => c.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var containerRuntime = new Mock<IContainerRuntime>();
        containerRuntime.Setup(r => r.CreateAsync(It.IsAny<ContainerProcessDefinition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(container.Object);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new ContainerRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ContainerRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should NOT wait for exit
        container.Verify(c => c.WaitForExitAsync(It.IsAny<CancellationToken>()), Times.Never);
        taskContext.Verify(
            c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

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

        var executor = new ContainerRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ContainerRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            containerRuntime.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(
            c => c.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new ContainerRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ContainerRunTaskExecutor>>(),
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
