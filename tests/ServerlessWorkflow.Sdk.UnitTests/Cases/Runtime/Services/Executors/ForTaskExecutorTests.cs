namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class ForTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Initialize_Should_Evaluate_Collection_Expression()
    {
        // Arrange
        var subtasks = new Map<string, TaskDefinition>();
        subtasks.Add(new("processItem", new SetTaskDefinition { Set = new JsonObject { ["processed"] = true } }));

        var definition = new ForTaskDefinition
        {
            For = new ForLoopDefinition { Each = "item", In = "${ .items }" },
            Do = subtasks
        };
        var input = new JsonObject { ["items"] = new JsonArray("a", "b", "c") };
        var taskContext = CreateTaskExecutionContext(definition, input);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.Is<string>(s => s.Contains("items")),
            It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new JsonArray("a", "b", "c"));

        var executor = new ForTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);

        // Assert
        expressionMock.Verify(
            e => e.EvaluateAsync(It.Is<string>(s => s.Contains("items")), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Initialize_Should_Fault_When_Collection_Is_Not_Array()
    {
        // Arrange
        var subtasks = new Map<string, TaskDefinition>();
        subtasks.Add(new("processItem", new SetTaskDefinition { Set = new JsonObject { ["processed"] = true } }));

        var definition = new ForTaskDefinition
        {
            For = new ForLoopDefinition { Each = "item", In = "${ .notAnArray }" },
            Do = subtasks
        };
        var taskContext = CreateTaskExecutionContext(definition);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new JsonObject { ["not"] = "an array" });

        var executor = new ForTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);

        // Assert - should set an error on the task instance because the expression didn't evaluate to an array
        taskContext.Verify(
            i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Create_Iteration_Task_For_First_Item()
    {
        // Arrange
        var subtasks = new Map<string, TaskDefinition>();
        subtasks.Add(new("processItem", new SetTaskDefinition { Set = new JsonObject { ["done"] = true } }));

        var definition = new ForTaskDefinition
        {
            For = new ForLoopDefinition { Each = "item", In = "${ .items }" },
            Do = subtasks
        };
        var input = new JsonObject { ["items"] = new JsonArray("a") };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.Is<string>(s => s.Contains("items")),
            It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new JsonArray("a"));

        var childExecutor = CreateCompletingChildExecutor();
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

        var executor = new ForTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForTaskExecutor>>(),
            contextFactory.Object,
            executorFactory.Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        await executor.InitializeAsync(TestContext.Current.CancellationToken);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Workflow).Verify(
            w => w.CreateTaskAsync(It.IsAny<TaskDefinition>(), It.IsAny<JsonPointer>(), It.IsAny<JsonNode>(), It.IsAny<ITaskExecutionContext?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var subtasks = new Map<string, TaskDefinition>();
        subtasks.Add(new("processItem", new SetTaskDefinition { Set = new JsonObject { ["done"] = true } }));

        var definition = new ForTaskDefinition
        {
            For = new ForLoopDefinition { Each = "item", In = "${ .items }" },
            Do = subtasks
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new ForTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ForTaskExecutor>>(),
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

    static Mock<ITaskExecutor> CreateCompletingChildExecutor(string path = "/for/0/do")
    {
        var childInstance = new Mock<ITaskInstance> { CallBase = true };
        childInstance.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        childInstance.Setup(s => s.Output).Returns(new JsonObject());
        childInstance.Setup(s => s.Next).Returns(FlowDirective.Continue);
        childInstance.Setup(s => s.Reference).Returns(JsonPointer.Parse(path));
        childInstance.Setup(s => s.IsOperative).Returns(false);

        var childWorkflowInstance = new Mock<IWorkflowInstance>();
        childWorkflowInstance.Setup(i => i.ContextData).Returns(new JsonObject());
        var childWorkflow = new Mock<IWorkflowExecutionContext>();
        childWorkflow.Setup(w => w.Instance).Returns(childWorkflowInstance.Object);
        var childTaskContext = new Mock<ITaskExecutionContext>();
        childTaskContext.Setup(c => c.Instance).Returns(childInstance.Object);
        childTaskContext.Setup(c => c.Workflow).Returns(childWorkflow.Object);
        childTaskContext.Setup(c => c.GetSubTasksAsync(It.IsAny<CancellationToken>())).Returns(AsyncEnumerableEmpty<ITaskInstance>());

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
