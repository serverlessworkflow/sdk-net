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
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var childExecutor = CreateCompletingChildExecutor(new JsonObject { ["name"] = "test" });
        var executorFactory = new Mock<ITaskExecutorFactory>();
        executorFactory.Setup(f => f.Create(It.IsAny<ITaskExecutionContext>())).Returns(childExecutor.Object);
        var contextFactory = new Mock<ITaskExecutionContextFactory>();
        contextFactory.Setup(f => f.Create(It.IsAny<IWorkflowExecutionContext>(), It.IsAny<TaskDefinition>(), It.IsAny<ITaskInstance>(), It.IsAny<JsonObject?>()))
            .Returns((IWorkflowExecutionContext wf, TaskDefinition def, ITaskInstance inst, JsonObject? args) =>
            {
                var childContext = new Mock<ITaskExecutionContext>();
                childContext.Setup(c => c.Workflow).Returns(wf);
                childContext.Setup(c => c.Instance).Returns(inst);
                childContext.Setup(c => c.Definition).Returns(def);
                childContext.Setup(c => c.Arguments).Returns(args ?? new JsonObject());
                return childContext.Object;
            });
        var executor = new DoTaskExecutor(CreateServiceProvider().Object, Mock.Of<ILogger<DoTaskExecutor>>(), contextFactory.Object, executorFactory.Object, CreateSchemaHandlerProvider().Object, taskContext.Object);
        // act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);
        // assert
        Mock.Get(taskContext.Object.Workflow).Verify(
            w => w.CreateTaskAsync(It.IsAny<TaskDefinition>(), It.IsAny<JsonPointer>(), It.IsAny<JsonNode>(), It.IsAny<ITaskExecutionContext?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Complete_When_No_Subtasks()
    {
        // arrange
        var subtasks = new Map<string, TaskDefinition>();
        var definition = new DoTaskDefinition { Do = subtasks };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
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
        taskContext.Verify(c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var subtasks = new Map<string, TaskDefinition>();
        subtasks.Add(new("task1", new SetTaskDefinition { Set = new JsonObject { ["k"] = "v" } }));
        var definition = new DoTaskDefinition { Do = subtasks };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
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
        taskContext.Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static Mock<ITaskExecutor> CreateCompletingChildExecutor(JsonNode? output = null, string? next = FlowDirective.Continue)
    {
        var childInstance = new Mock<ITaskInstance> { CallBase = true };
        childInstance.Setup<string?>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        childInstance.Setup(s => s.Output).Returns(output);
        childInstance.Setup(s => s.Next).Returns(next);
        childInstance.Setup(s => s.Reference).Returns(JsonPointer.Parse("/sub"));
        childInstance.Setup(s => s.Name).Returns("sub");

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
