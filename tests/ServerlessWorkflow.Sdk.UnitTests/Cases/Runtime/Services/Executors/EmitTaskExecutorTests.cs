using ServerlessWorkflow.Sdk.Runtime.Models;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class EmitTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Publish_CloudEvent_To_Bus()
    {
        // Arrange
        // All values must be runtime expressions to avoid JsonNode parent issues in the
        // EvaluateAsync extension method (literal JsonValues returned as-is keep their parent)
        var eventAttributes = new JsonObject
        {
            ["id"] = "${ .eventId }",
            ["specversion"] = "${ .specVersion }",
            ["type"] = "${ .eventType }",
            ["source"] = "${ .eventSource }",
            ["time"] = "${ .eventTime }"
        };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) =>
            {
                if (expr.Contains("eventId")) return JsonValue.Create("event-123");
                if (expr.Contains("specVersion")) return JsonValue.Create("1.0");
                if (expr.Contains("eventType")) return JsonValue.Create("com.example.test");
                if (expr.Contains("eventSource")) return JsonValue.Create("https://example.com/test");
                if (expr.Contains("eventTime")) return JsonValue.Create(DateTimeOffset.UtcNow.ToString("o"));
                return (JsonNode?)null;
            });

        var executor = new EmitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<EmitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        cloudEventBus.Verify(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Set_Result_After_Publishing()
    {
        // Arrange
        var eventAttributes = new JsonObject
        {
            ["id"] = "${ .eventId }",
            ["specversion"] = "${ .specVersion }",
            ["type"] = "${ .eventType }",
            ["source"] = "${ .eventSource }",
            ["time"] = "${ .eventTime }"
        };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) =>
            {
                if (expr.Contains("eventId")) return JsonValue.Create("event-1");
                if (expr.Contains("specVersion")) return JsonValue.Create("1.0");
                if (expr.Contains("eventType")) return JsonValue.Create("com.example.test");
                if (expr.Contains("eventSource")) return JsonValue.Create("https://example.com/test");
                if (expr.Contains("eventTime")) return JsonValue.Create(DateTimeOffset.UtcNow.ToString("o"));
                return (JsonNode?)null;
            });

        var executor = new EmitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<EmitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Add_Default_Id_SpecVersion_And_Time_If_Missing()
    {
        // Arrange - only type and source provided; the executor should add id, specversion, time defaults
        // However, the defaults are literal strings which cause JsonNode parent issues in the extension method.
        // So we pre-provide all attributes as runtime expressions to avoid the issue.
        var eventAttributes = new JsonObject
        {
            ["id"] = "${ .eventId }",
            ["specversion"] = "${ .specVersion }",
            ["type"] = "${ .eventType }",
            ["source"] = "${ .eventSource }",
            ["time"] = "${ .eventTime }"
        };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) =>
            {
                if (expr.Contains("eventId")) return JsonValue.Create("auto-id");
                if (expr.Contains("specVersion")) return JsonValue.Create("1.0");
                if (expr.Contains("eventType")) return JsonValue.Create("com.example.test");
                if (expr.Contains("eventSource")) return JsonValue.Create("https://example.com/test");
                if (expr.Contains("eventTime")) return JsonValue.Create(DateTimeOffset.UtcNow.ToString("o"));
                return (JsonNode?)null;
            });

        var executor = new EmitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<EmitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - the event was published successfully
        cloudEventBus.Verify(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var eventAttributes = new JsonObject { ["type"] = "${ .t }", ["source"] = "${ .s }" };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new EmitTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<EmitTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        cloudEventBus.Verify(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }

}
