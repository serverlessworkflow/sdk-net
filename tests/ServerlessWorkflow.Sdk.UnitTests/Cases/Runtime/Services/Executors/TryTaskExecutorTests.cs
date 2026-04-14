namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class TryTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Create_Try_Subtask()
    {
        // Arrange
        var tryTasks = new Map<string, TaskDefinition>();
        tryTasks.Add(new("riskyTask", new SetTaskDefinition { Set = new JsonObject { ["result"] = "ok" } }));

        var definition = new TryTaskDefinition
        {
            Try = tryTasks,
            Catch = new ErrorCatcherDefinition()
        };
        var input = new JsonObject { ["data"] = "test" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var childExecutor = CreateCompletingChildExecutor(new JsonObject { ["result"] = "ok" });
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(childExecutor.Object);

        var contextFactory = new Mock<ITaskExecutionContextFactory>();
        contextFactory.Setup(f => f.Create(
            It.IsAny<IWorkflowExecutionContext>(),
            It.IsAny<ITaskInstance>(),
            It.IsAny<TaskDefinition>(),
            It.IsAny<JsonObject>(),
            It.IsAny<JsonObject?>()))
            .Returns((IWorkflowExecutionContext wf, ITaskInstance inst, TaskDefinition def, JsonObject ctx, JsonObject? args) =>
            {
                var childCtx = new Mock<ITaskExecutionContext>();
                childCtx.Setup(c => c.Workflow).Returns(wf);
                childCtx.Setup(c => c.Instance).Returns(inst);
                childCtx.Setup(c => c.Definition).Returns(def);
                childCtx.Setup(c => c.ContextData).Returns(ctx);
                childCtx.Setup(c => c.Arguments).Returns(args ?? new JsonObject());
                childCtx.Setup(c => c.Input).Returns(new JsonObject());
                return childCtx.Object;
            });

        var executor = new TryTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<TryTaskExecutor>>(),
            contextFactory.Object,
            executorFactory.Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Workflow.Instance).Verify(
            i => i.CreateTaskAsync(
                It.IsAny<TaskDefinition>(),
                It.Is<string?>(s => s == "try"),
                It.IsAny<JsonNode>(),
                It.IsAny<JsonObject?>(),
                It.IsAny<ITaskInstance?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Set_Result_When_Try_Succeeds()
    {
        // Arrange
        var tryTasks = new Map<string, TaskDefinition>();
        tryTasks.Add(new("successTask", new SetTaskDefinition { Set = new JsonObject { ["result"] = "success" } }));

        var definition = new TryTaskDefinition
        {
            Try = tryTasks,
            Catch = new ErrorCatcherDefinition()
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var childExecutor = CreateCompletingChildExecutor(new JsonObject { ["result"] = "success" });
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(childExecutor.Object);

        var contextFactory = new Mock<ITaskExecutionContextFactory>();
        contextFactory.Setup(f => f.Create(
            It.IsAny<IWorkflowExecutionContext>(),
            It.IsAny<ITaskInstance>(),
            It.IsAny<TaskDefinition>(),
            It.IsAny<JsonObject>(),
            It.IsAny<JsonObject?>()))
            .Returns((IWorkflowExecutionContext wf, ITaskInstance inst, TaskDefinition def, JsonObject ctx, JsonObject? args) =>
            {
                var childCtx = new Mock<ITaskExecutionContext>();
                childCtx.Setup(c => c.Workflow).Returns(wf);
                childCtx.Setup(c => c.Instance).Returns(inst);
                childCtx.Setup(c => c.Definition).Returns(def);
                childCtx.Setup(c => c.ContextData).Returns(ctx);
                childCtx.Setup(c => c.Arguments).Returns(args ?? new JsonObject());
                childCtx.Setup(c => c.Input).Returns(new JsonObject());
                return childCtx.Object;
            });

        var executor = new TryTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<TryTaskExecutor>>(),
            contextFactory.Object,
            executorFactory.Object,
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
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var tryTasks = new Map<string, TaskDefinition>();
        tryTasks.Add(new("task", new SetTaskDefinition { Set = new JsonObject { ["k"] = "v" } }));

        var definition = new TryTaskDefinition
        {
            Try = tryTasks,
            Catch = new ErrorCatcherDefinition()
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new TryTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<TryTaskExecutor>>(),
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

    static Mock<ITaskExecutor> CreateCompletingChildExecutor(JsonNode? output = null)
    {
        var childState = new Mock<ITaskInstance>();
        childState.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        childState.Setup(s => s.Output).Returns(output);
        childState.Setup(s => s.Next).Returns(FlowDirective.Continue);
        childState.Setup(s => s.Reference).Returns(JsonPointer.Parse("/try"));
        childState.Setup(s => s.Name).Returns("try");

        var childInstance = new Mock<ITaskInstance>();
        childInstance.Setup(i => i.State).Returns(childState.Object);

        var childTaskContext = new Mock<ITaskExecutionContext>();
        childTaskContext.Setup(c => c.Instance).Returns(childInstance.Object);
        childTaskContext.Setup(c => c.Output).Returns(output);
        childTaskContext.Setup(c => c.ContextData).Returns(new JsonObject());

        var childExecutor = new Mock<ITaskExecutor>();
        childExecutor.Setup(e => e.Task).Returns(childTaskContext.Object);
        childExecutor.Setup(e => e.InitializeAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.ExecuteAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.Subscribe(It.IsAny<IObserver<ITaskLifeCycleEvent>>()))
            .Returns((IObserver<ITaskLifeCycleEvent> observer) =>
            {
                observer.OnCompleted();
                return Mock.Of<IDisposable>();
            });

        return childExecutor;
    }

}
