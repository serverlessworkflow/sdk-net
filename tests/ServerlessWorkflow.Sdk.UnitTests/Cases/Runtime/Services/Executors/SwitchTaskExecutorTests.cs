namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class SwitchTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Match_First_True_Case()
    {
        // Arrange
        var switchCases = new Map<string, SwitchCaseDefinition>();
        switchCases.Add(new("caseA", new SwitchCaseDefinition { When = "${ .status == \"active\" }", Then = FlowDirective.End }));
        switchCases.Add(new("caseB", new SwitchCaseDefinition { When = "${ .status == \"inactive\" }", Then = FlowDirective.Continue }));

        var definition = new SwitchTaskDefinition { Switch = switchCases };
        var input = new JsonObject { ["status"] = "active" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        // First case matches
        expressionMock.Setup(e => e.EvaluateAsync(
            It.Is<string>(s => s.Contains("active")),
            It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonValue.Create(true));
        // Second case does not match
        expressionMock.Setup(e => e.EvaluateAsync(
            It.Is<string>(s => s.Contains("inactive")),
            It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonValue.Create(false));

        var executor = new SwitchTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<SwitchTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Use_Default_Case_When_No_Condition_Matches()
    {
        // Arrange
        var switchCases = new Map<string, SwitchCaseDefinition>();
        switchCases.Add(new("caseA", new SwitchCaseDefinition { When = "${ .status == \"active\" }", Then = FlowDirective.End }));
        switchCases.Add(new("default", new SwitchCaseDefinition { Then = FlowDirective.Continue }));

        var definition = new SwitchTaskDefinition { Switch = switchCases };
        var input = new JsonObject { ["status"] = "unknown" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        // No cases match
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonValue.Create(false));

        var executor = new SwitchTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<SwitchTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Use_Task_Then_When_No_Case_Matches_And_No_Default()
    {
        // Arrange
        var switchCases = new Map<string, SwitchCaseDefinition>();
        switchCases.Add(new("caseA", new SwitchCaseDefinition { When = "${ .status == \"active\" }", Then = FlowDirective.End }));

        var definition = new SwitchTaskDefinition { Switch = switchCases, Then = FlowDirective.Continue };
        var input = new JsonObject { ["status"] = "unknown" };
        var taskContext = CreateTaskExecutionContext(definition, input);

        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonValue.Create(false));

        var executor = new SwitchTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<SwitchTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var switchCases = new Map<string, SwitchCaseDefinition>();
        switchCases.Add(new("caseA", new SwitchCaseDefinition { When = "${ true }", Then = FlowDirective.End }));

        var definition = new SwitchTaskDefinition { Switch = switchCases };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var executor = new SwitchTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<SwitchTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

}
