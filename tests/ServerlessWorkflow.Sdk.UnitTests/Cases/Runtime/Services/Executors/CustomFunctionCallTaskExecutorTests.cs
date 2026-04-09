namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class CustomFunctionCallTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Resolve_Function_From_Workflow_Definition()
    {
        // arrange
        var innerTask = new SetTaskDefinition { Set = new JsonObject { ["result"] = "ok" } };
        var definition = new CallTaskDefinition { Call = "myFunction" };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var workflowDef = new WorkflowDefinition
        {
            Document = new WorkflowDefinitionMetadata { Dsl = "1.0.0", Name = "test", Namespace = "test", Version = "1.0.0" },
            Do = [],
            Use = new ComponentDefinitionCollection { Functions = new EquatableDictionary<string, TaskDefinition> { ["myFunction"] = innerTask } }
        };
        Mock.Get(taskContext.Object.Workflow).Setup(w => w.Definition).Returns(workflowDef);
        var subExecutor = new Mock<ITaskExecutor>();
        subExecutor.Setup(e => e.Task).Returns(taskContext.Object);
        subExecutor.Setup(e => e.InitializeAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        subExecutor.Setup(e => e.ExecuteAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        subExecutor.Setup(e => e.Subscribe(It.IsAny<IObserver<ITaskLifeCycleEvent>>())).Returns(Mock.Of<IDisposable>());
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(subExecutor.Object);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object, executorFactory.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Workflow.Instance).Verify(
            i => i.CreateTaskAsync(It.IsAny<TaskDefinition>(), It.IsAny<string?>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<ITaskInstance?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Set_Error_When_Function_Not_Found()
    {
        // arrange
        var definition = new CallTaskDefinition { Call = "unknownFunction" };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var definition = new CallTaskDefinition { Call = "myFunction" };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static CustomFunctionCallTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<CallTaskDefinition>> taskContext, IHttpClientFactory httpClientFactory, IAuthenticationHandler authHandler, ITaskExecutorFactory? executorFactory = null) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<CustomFunctionCallTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        executorFactory ?? CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        httpClientFactory,
        authHandler,
        taskContext.Object);

}
