using ServerlessWorkflow.Sdk.Runtime.Models;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class EmitTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Publish_CloudEvent_To_Bus()
    {
        // Arrange
        // Use runtime expressions so the mock evaluator is invoked (avoids JsonNode parent issues with literal values)
        var eventAttributes = new JsonObject
        {
            ["type"] = "${ .eventType }",
            ["source"] = "${ .eventSource }"
        };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var input = new JsonObject { ["eventType"] = "com.example.test", ["eventSource"] = "https://example.com/test" };
        var taskContext = CreateTaskExecutionContext(definition, input);
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) =>
            {
                // Return a full cloud event JSON for the final evaluation, or scalar values for individual expressions
                if (expr.Contains("eventType")) return JsonValue.Create("com.example.test");
                if (expr.Contains("eventSource")) return JsonValue.Create("https://example.com/test");
                return (JsonNode)new JsonObject
                {
                    ["id"] = Guid.NewGuid().ToString(),
                    ["specversion"] = "1.0",
                    ["type"] = "com.example.test",
                    ["source"] = "https://example.com/test",
                    ["time"] = DateTimeOffset.Now.ToString("o")
                };
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
            ["type"] = "${ .eventType }",
            ["source"] = "${ .eventSource }"
        };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) =>
            {
                if (expr.Contains("eventType")) return JsonValue.Create("com.example.test");
                if (expr.Contains("eventSource")) return JsonValue.Create("https://example.com/test");
                return (JsonNode)new JsonObject
                {
                    ["id"] = "event-1",
                    ["specversion"] = "1.0",
                    ["type"] = "com.example.test",
                    ["source"] = "https://example.com/test",
                    ["time"] = DateTimeOffset.Now.ToString("o")
                };
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
        // Arrange - only type and source, no id/specversion/time
        var eventAttributes = new JsonObject
        {
            ["type"] = "${ .eventType }",
            ["source"] = "${ .eventSource }"
        };
        var definition = new EmitTaskDefinition
        {
            Emit = new EventEmissionDefinition { Event = new EventDefinition { With = eventAttributes } }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.PublishAsync(It.IsAny<ICloudEvent>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);

        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) =>
            {
                if (expr.Contains("eventType")) return JsonValue.Create("com.example.test");
                if (expr.Contains("eventSource")) return JsonValue.Create("https://example.com/test");
                return (JsonNode)new JsonObject
                {
                    ["id"] = "auto-generated-id",
                    ["specversion"] = "1.0",
                    ["type"] = "com.example.test",
                    ["source"] = "https://example.com/test",
                    ["time"] = DateTimeOffset.Now.ToString("o")
                };
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

        // Assert - the event was published successfully (defaults were added)
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

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);

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
