namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class DoTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Execute_First_Subtask()
    {
        // arrange
        var subtasks = new Map<string, TaskDefinition>
        {
            new("setName", new SetTaskDefinition { Set = new JsonObject { ["name"] = "test" } })
        };
        var definition = new DoTaskDefinition { Do = subtasks };
        var input = new JsonObject { ["initial"] = true };
        var taskContext = CreateTaskExecutionContext(definition, input);
        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var childExecutor = CreateCompletingChildExecutor(new JsonObject { ["name"] = "test" });
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(childExecutor.Object);
        var contextFactory = new Mock<ITaskExecutionContextFactory>();
        contextFactory.Setup(f => f.Create(It.IsAny<IWorkflowExecutionContext>(), It.IsAny<ITaskInstance>(), It.IsAny<TaskDefinition>(), It.IsAny<JsonObject>(), It.IsAny<JsonObject?>()))
            .Returns((IWorkflowExecutionContext wf, ITaskInstance inst, TaskDefinition def, JsonObject ctx, JsonObject? args) =>
            {
                var childContext = new Mock<ITaskExecutionContext>();
                childContext.Setup(c => c.Workflow).Returns(wf);
                childContext.Setup(c => c.Instance).Returns(inst);
                childContext.Setup(c => c.Definition).Returns(def);
                childContext.Setup(c => c.ContextData).Returns(ctx);
                childContext.Setup(c => c.Arguments).Returns(args ?? new JsonObject());
                childContext.Setup(c => c.Input).Returns(new JsonObject());
                return childContext.Object;
            });
        var executor = new DoTaskExecutor(CreateServiceProvider().Object, Mock.Of<ILogger<DoTaskExecutor>>(), contextFactory.Object, executorFactory.Object, CreateSchemaHandlerProvider().Object, taskContext.Object);
        // act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);
        // assert
        Mock.Get(taskContext.Object.Workflow.Instance).Verify(
            i => i.CreateTaskAsync(It.IsAny<TaskDefinition>(), It.IsAny<string?>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<ITaskInstance?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Complete_When_No_Subtasks()
    {
        // arrange
        var subtasks = new Map<string, TaskDefinition>();
        var definition = new DoTaskDefinition { Do = subtasks };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var executor = new DoTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<DoTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);
        // act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);
        // assert
        Mock.Get(taskContext.Object.Instance).Verify(i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var subtasks = new Map<string, TaskDefinition>();
        subtasks.Add(new("task1", new SetTaskDefinition { Set = new JsonObject { ["k"] = "v" } }));
        var definition = new DoTaskDefinition { Do = subtasks };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        var executor = new DoTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<DoTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);
        // act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);
        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static Mock<ITaskExecutor> CreateCompletingChildExecutor(JsonNode? output = null, string? next = FlowDirective.Continue)
    {
        var childState = new Mock<ITaskInstance>();
        childState.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        childState.Setup(s => s.Output).Returns(output);
        childState.Setup(s => s.Next).Returns(next);
        childState.Setup(s => s.Reference).Returns(JsonPointer.Parse("/sub"));
        childState.Setup(s => s.Name).Returns("sub");

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
