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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var childExecutor = CreateCompletingChildExecutor(new JsonObject { ["result"] = "ok" });
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(childExecutor.Object);

        var contextFactory = new Mock<ITaskExecutionContextFactory>();
        contextFactory.Setup(f => f.Create(
            It.IsAny<IWorkflowExecutionContext>(),
            It.IsAny<TaskDefinition>(),
            It.IsAny<ITaskInstance>(),
            It.IsAny<JsonObject?>()))
            .Returns((IWorkflowExecutionContext wf, TaskDefinition def, ITaskInstance inst, JsonObject? args) =>
            {
                var childCtx = new Mock<ITaskExecutionContext>();
                childCtx.Setup(c => c.Workflow).Returns(wf);
                childCtx.Setup(c => c.Instance).Returns(inst);
                childCtx.Setup(c => c.Definition).Returns(def);
                childCtx.Setup(c => c.Arguments).Returns(args ?? new JsonObject());
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
        Mock.Get(taskContext.Object.Workflow).Verify(
            w => w.CreateTaskAsync(
                It.IsAny<TaskDefinition>(),
                It.IsAny<JsonPointer>(),
                It.IsAny<JsonNode>(),
                It.IsAny<ITaskExecutionContext?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var childExecutor = CreateCompletingChildExecutor(new JsonObject { ["result"] = "success" });
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(childExecutor.Object);

        var contextFactory = new Mock<ITaskExecutionContextFactory>();
        contextFactory.Setup(f => f.Create(
            It.IsAny<IWorkflowExecutionContext>(),
            It.IsAny<TaskDefinition>(),
            It.IsAny<ITaskInstance>(),
            It.IsAny<JsonObject?>()))
            .Returns((IWorkflowExecutionContext wf, TaskDefinition def, ITaskInstance inst, JsonObject? args) =>
            {
                var childCtx = new Mock<ITaskExecutionContext>();
                childCtx.Setup(c => c.Workflow).Returns(wf);
                childCtx.Setup(c => c.Instance).Returns(inst);
                childCtx.Setup(c => c.Definition).Returns(def);
                childCtx.Setup(c => c.Arguments).Returns(args ?? new JsonObject());
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
        taskContext.Verify(
            c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
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

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

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
        taskContext.Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static Mock<ITaskExecutor> CreateCompletingChildExecutor(JsonNode? output = null)
    {
        var childInstance = new Mock<ITaskInstance> { CallBase = true };
        childInstance.Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        childInstance.Setup(i => i.Output).Returns(output);
        childInstance.Setup(i => i.Next).Returns(FlowDirective.Continue);
        childInstance.Setup(i => i.Reference).Returns(JsonPointer.Parse("/try"));
        childInstance.Setup(i => i.Name).Returns("try");

        var childWorkflowInstance = new Mock<IWorkflowInstance>();
        childWorkflowInstance.Setup(i => i.ContextData).Returns(new JsonObject());
        var childWorkflow = new Mock<IWorkflowExecutionContext>();
        childWorkflow.Setup(w => w.Instance).Returns(childWorkflowInstance.Object);
        var childTaskContext = new Mock<ITaskExecutionContext>();
        childTaskContext.Setup(c => c.Instance).Returns(childInstance.Object);
        childTaskContext.Setup(c => c.Workflow).Returns(childWorkflow.Object);

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
