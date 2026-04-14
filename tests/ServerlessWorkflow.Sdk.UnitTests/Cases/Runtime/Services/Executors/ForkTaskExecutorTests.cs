namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class ForkTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Create_Branch_Tasks_For_All_Branches()
    {
        // Arrange
        var branches = new Map<string, TaskDefinition>();
        branches.Add(new("branchA", new SetTaskDefinition { Set = new JsonObject { ["a"] = "${ .a }" } }));
        branches.Add(new("branchB", new SetTaskDefinition { Set = new JsonObject { ["b"] = "${ .b }" } }));

        var definition = new ForkTaskDefinition
        {
            Fork = new BranchingDefinition { Branches = branches, Compete = false }
        };
        var input = new JsonObject { ["data"] = "test" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        // Track how many times CreateTaskAsync is called
        var createCount = 0;

        // Make the child executor's Subscribe fire OnCompleted asynchronously (after a yield)
        // to avoid deadlocking with the AsyncLock in OnSubTaskCompletedAsync
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>()))
            .Returns(() =>
            {
                var branchIndex = Interlocked.Increment(ref createCount) - 1;
                return CreateAsyncCompletingChildExecutor(branchIndex).Object;
            });

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

        // Also set up GetSubTasksAsync to return completed subtasks so allDone becomes true
        Mock.Get(taskContext.Object.Instance)
            .Setup(i => i.GetSubTasksAsync(It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                var completedState = new Mock<ITaskInstance> { CallBase = true };
                completedState.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
                var sub1 = new Mock<ITaskInstance>();
                sub1.Setup(i => i.State).Returns(completedState.Object);
                var sub2 = new Mock<ITaskInstance>();
                sub2.Setup(i => i.State).Returns(completedState.Object);
                return AsyncEnumerableOf(sub1.Object, sub2.Object);
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
        branches.Add(new("branchA", new SetTaskDefinition { Set = new JsonObject { ["a"] = "${ .a }" } }));

        var definition = new ForkTaskDefinition
        {
            Fork = new BranchingDefinition { Branches = branches }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

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
    public async Task Execute_Compete_Should_Complete_When_First_Branch_Finishes()
    {
        // Arrange
        var branches = new Map<string, TaskDefinition>();
        branches.Add(new("fast", new SetTaskDefinition { Set = new JsonObject { ["fast"] = "${ .f }" } }));
        branches.Add(new("slow", new SetTaskDefinition { Set = new JsonObject { ["slow"] = "${ .s }" } }));

        var definition = new ForkTaskDefinition
        {
            Fork = new BranchingDefinition { Branches = branches, Compete = true }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var createCount = 0;
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>()))
            .Returns(() =>
            {
                var idx = Interlocked.Increment(ref createCount) - 1;
                return CreateAsyncCompletingChildExecutor(idx).Object;
            });

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

        // Assert - in compete mode, SetResultAsync should be called when the first branch completes
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    /// <summary>
    /// Creates a child executor mock whose Subscribe fires OnCompleted asynchronously
    /// (via Task.Yield) to avoid deadlocking with the ForkTaskExecutor's AsyncLock.
    /// </summary>
    static Mock<ITaskExecutor> CreateAsyncCompletingChildExecutor(int index)
    {
        var childState = new Mock<ITaskInstance> { CallBase = true };
        childState.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        childState.Setup(s => s.Output).Returns(new JsonObject());
        childState.Setup(s => s.Next).Returns(FlowDirective.Continue);
        childState.Setup(s => s.Reference).Returns(JsonPointer.Parse($"/fork/branches/{index}/branch"));
        childState.Setup(s => s.Name).Returns($"branch{index}");

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
        childExecutor.Setup(e => e.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.DisposeAsync()).Returns(ValueTask.CompletedTask);

        // ExecuteAsync completes synchronously, but Subscribe fires OnCompleted
        // asynchronously to let the WhenAll and TaskCompletionSource wire up first.
        childExecutor.Setup(e => e.ExecuteAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        childExecutor.Setup(e => e.Subscribe(It.IsAny<IObserver<ITaskLifeCycleEvent>>()))
            .Returns((IObserver<ITaskLifeCycleEvent> observer) =>
            {
                // Fire completion on a background thread after yielding
                _ = Task.Run(async () =>
                {
                    await Task.Yield();
                    observer.OnCompleted();
                });
                return Mock.Of<IDisposable>();
            });

        return childExecutor;
    }

}
