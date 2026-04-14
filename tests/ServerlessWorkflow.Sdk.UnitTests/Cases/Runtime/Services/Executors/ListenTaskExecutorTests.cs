using System.Reactive.Linq;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class ListenTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Subscribe_To_CloudEventBus()
    {
        // Arrange
        var definition = new ListenTaskDefinition
        {
            Listen = new ListenerDefinition
            {
                To = new EventConsumptionStrategyDefinition()
            }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var eventSubject = new ReplaySubject<ICloudEvent>();
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.SubscribeAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventSubject.AsObservable());

        // Emit an event so the listen executor can complete
        var cloudEvent = new CloudEvent
        {
            Source = new Uri("https://example.com"),
            Type = "com.example.test"
        };
        eventSubject.OnNext(cloudEvent);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new ListenTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ListenTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        cloudEventBus.Verify(b => b.SubscribeAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Collect_Events_And_Set_Result()
    {
        // Arrange
        var definition = new ListenTaskDefinition
        {
            Listen = new ListenerDefinition
            {
                To = new EventConsumptionStrategyDefinition()
            }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var eventSubject = new ReplaySubject<ICloudEvent>();
        var cloudEventBus = new Mock<ICloudEventBus>();
        cloudEventBus.Setup(b => b.SubscribeAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventSubject.AsObservable());

        var cloudEvent = new CloudEvent
        {
            Source = new Uri("https://example.com"),
            Type = "com.example.test"
        };
        eventSubject.OnNext(cloudEvent);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var executor = new ListenTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ListenTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should set result with collected events
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var definition = new ListenTaskDefinition
        {
            Listen = new ListenerDefinition
            {
                To = new EventConsumptionStrategyDefinition()
            }
        };
        var taskContext = CreateTaskExecutionContext(definition);
        var cloudEventBus = new Mock<ICloudEventBus>();

        Mock.Get(taskContext.Object.Instance.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new ListenTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ListenTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            cloudEventBus.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        cloudEventBus.Verify(b => b.SubscribeAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

}
