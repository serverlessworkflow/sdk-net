namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public abstract class TaskExecutorTestsBase
{

    protected static Mock<ITaskExecutionContext<TDefinition>> CreateTaskExecutionContext<TDefinition>(
        TDefinition definition, JsonNode? input = null, JsonObject? contextData = null,
        JsonObject? arguments = null, string? taskStatus = null, string? taskName = null, JsonPointer? reference = null)
        where TDefinition : TaskDefinition
    {
        input ??= new JsonObject();
        contextData ??= [];
        arguments ??= [];
        var effectiveReference = reference ?? JsonPointer.Parse("/test");

        var taskState = new Mock<ITaskInstance> { CallBase = true };
        taskState.Setup(s => s.Status).Returns(taskStatus);
        taskState.Setup(s => s.Name).Returns(taskName);
        taskState.Setup(s => s.Reference).Returns(effectiveReference);
        taskState.Setup(s => s.IsExtension).Returns(false);

        var taskInstance = new Mock<ITaskInstance>();
        taskInstance.Setup(i => i.State).Returns(taskState.Object);
        taskInstance.Setup(i => i.InitializeAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SetContextDataAsync(It.IsAny<JsonObject>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SuspendAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SkipAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.GetSubTasksAsync(It.IsAny<CancellationToken>())).Returns(AsyncEnumerableEmpty<ITaskInstance>());

        var expressionEvaluator = new Mock<IRuntimeExpressionEvaluator>();
        expressionEvaluator.Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((JsonNode?)null);

        var runtimeDescriptor = new RuntimeDescriptor() { Name = "test-runtime", Version = "1.0.0" };
        var runtime = new Mock<IWorkflowRuntime>();
        runtime.Setup(r => r.Descriptor).Returns(runtimeDescriptor);

        var workflowDefinition = new WorkflowDefinition
        {
            Document = new WorkflowDefinitionMetadata
            {
                Dsl = "1.0.0",
                Name = "test-workflow",
                Namespace = "test",
                Version = "1.0.0"
            },
            Do = []
        };

        var workflowState = new Mock<IWorkflowInstance>();
        workflowState.Setup(s => s.Id).Returns("workflow-1");

        var workflowInstance = new Mock<IWorkflowInstance>();
        workflowInstance.Setup(i => i.State).Returns(workflowState.Object);
        workflowInstance.Setup(i => i.CreateTaskAsync(
            It.IsAny<TaskDefinition>(), It.IsAny<string?>(), It.IsAny<JsonNode>(),
            It.IsAny<JsonObject?>(), It.IsAny<ITaskInstance?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns((TaskDefinition def, string? path, JsonNode inp, JsonObject? ctx, ITaskInstance? parent, bool isExt, CancellationToken ct) =>
            {
                var subState = new Mock<ITaskInstance> { CallBase = true };
                subState.Setup(s => s.Reference).Returns(JsonPointer.Parse($"/{path ?? "sub"}"));
                subState.Setup(s => s.Name).Returns(path?.Split('/').Last());
                subState.Setup(s => s.IsExtension).Returns(isExt);
                var subInstance = new Mock<ITaskInstance>();
                subInstance.As<ITaskInstance<ITaskInstance>>().Setup(i => i.State).Returns(subState.Object);
                subInstance.Setup(i => i.State).Returns(subState.Object);
                subInstance.Setup(i => i.InitializeAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SetContextDataAsync(It.IsAny<JsonObject>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SuspendAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.GetSubTasksAsync(It.IsAny<CancellationToken>())).Returns(AsyncEnumerableEmpty<ITaskInstance>());
                return Task.FromResult(subInstance.As<ITaskInstance<ITaskInstance>>().Object);
            });

        var workflow = new Mock<IWorkflowExecutionContext>();
        workflow.Setup(w => w.Definition).Returns(workflowDefinition);
        workflow.Setup(w => w.Instance).Returns(workflowInstance.Object);
        workflow.Setup(w => w.Expressions).Returns(expressionEvaluator.Object);
        workflow.Setup(w => w.Runtime).Returns(runtime.Object);
        workflow.Setup(w => w.ContextData).Returns(new JsonObject());
        workflow.Setup(w => w.Arguments).Returns(new JsonObject());

        var taskContext = new Mock<ITaskExecutionContext<TDefinition>>();
        taskContext.Setup(c => c.Workflow).Returns(workflow.Object);
        taskContext.Setup(c => c.Definition).Returns(definition);
        taskContext.Setup(c => c.Instance).Returns(taskInstance.Object);
        taskContext.Setup(c => c.Input).Returns(() => input.DeepClone());
        taskContext.Setup(c => c.ContextData).Returns(() => contextData.DeepClone().AsObject()!);
        taskContext.Setup(c => c.Arguments).Returns(() => arguments.DeepClone().AsObject()!);
        taskContext.Setup(c => c.Output).Returns((JsonNode?)null);

        return taskContext;
    }

    protected static Mock<IServiceProvider> CreateServiceProvider() => new();

    protected static Mock<ITaskExecutionContextFactory> CreateExecutionContextFactory() => new();

    protected static Mock<ITaskExecutorFactory> CreateExecutorFactory() => new();

    protected static Mock<ISchemaHandlerProvider> CreateSchemaHandlerProvider() => new();

    protected static async IAsyncEnumerable<T> AsyncEnumerableEmpty<T>()
    {
        await Task.CompletedTask;
        yield break;
    }

    protected static async IAsyncEnumerable<T> AsyncEnumerableOf<T>(params T[] items)
    {
        foreach (var item in items)
        {
            yield return item;
            await Task.CompletedTask;
        }
    }

}
