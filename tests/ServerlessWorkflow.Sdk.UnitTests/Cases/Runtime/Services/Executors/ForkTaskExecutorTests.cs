namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class ForkTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Create_Branch_Tasks_For_All_Branches()
    {
        // Arrange
        var branches = new Map<string, TaskDefinition>();
        branches.Add(new("branchA", new SetTaskDefinition { Set = new JsonObject { ["a"] = 1 } }));
        branches.Add(new("branchB", new SetTaskDefinition { Set = new JsonObject { ["b"] = 2 } }));

        var definition = new ForkTaskDefinition
        {
            Fork = new BranchingDefinition { Branches = branches }
        };
        var input = new JsonObject { ["data"] = "test" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        // Set up the child executor
        var childExecutor = CreateCompletingChildExecutor();
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

        var executor = new ForkTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForkTaskExecutor>>(),
            contextFactory.Object,
            executorFactory.Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should create tasks for both branches
        Mock.Get(taskContext.Object.Workflow.Instance).Verify(
            i => i.CreateTaskAsync(It.IsAny<TaskDefinition>(), It.IsAny<string?>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<ITaskInstance?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.Exactly(2));
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var branches = new Map<string, TaskDefinition>();
        branches.Add(new("branchA", new SetTaskDefinition { Set = new JsonObject { ["a"] = 1 } }));

        var definition = new ForkTaskDefinition
        {
            Fork = new BranchingDefinition { Branches = branches }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);

        var executor = new ForkTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForkTaskExecutor>>(),
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

    [Fact]
    public async Task Execute_Should_Handle_Empty_Branches()
    {
        // Arrange
        var branches = new Map<string, TaskDefinition>();

        var definition = new ForkTaskDefinition
        {
            Fork = new BranchingDefinition { Branches = branches }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        var executor = new ForkTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForkTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act - with no branches, WhenAll resolves immediately, then the executor awaits TaskCompletionSource
        // This should not throw
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);
    }

    static Mock<ITaskExecutor> CreateCompletingChildExecutor()
    {
        var childState = new Mock<ITaskState>();
        childState.Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);
        childState.Setup(s => s.Output).Returns(new JsonObject());
        childState.Setup(s => s.Next).Returns(FlowDirective.Continue);
        childState.Setup(s => s.Reference).Returns(JsonPointer.Parse("/fork/branches/0/branchA"));

        var childInstance = new Mock<ITaskInstance>();
        childInstance.Setup(i => i.State).Returns(childState.Object);
        childInstance.Setup(i => i.GetSubTasksAsync(It.IsAny<CancellationToken>())).Returns(AsyncEnumerableEmpty<ITaskInstance>());

        var childTaskContext = new Mock<ITaskExecutionContext>();
        childTaskContext.Setup(c => c.Instance).Returns(childInstance.Object);
        childTaskContext.Setup(c => c.Output).Returns(new JsonObject());
        childTaskContext.Setup(c => c.ContextData).Returns(new JsonObject());

        var childExecutor = new Mock<ITaskExecutor>();
        childExecutor.Setup(e => e.Task).Returns(childTaskContext.Object);
        childExecutor.Setup(e => e.InitializeAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.ExecuteAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.DisposeAsync()).Returns(ValueTask.CompletedTask);
        childExecutor.Setup(e => e.Subscribe(It.IsAny<IObserver<ITaskLifeCycleEvent>>()))
            .Returns((IObserver<ITaskLifeCycleEvent> observer) =>
            {
                observer.OnCompleted();
                return Mock.Of<IDisposable>();
            });

        return childExecutor;
    }

}
