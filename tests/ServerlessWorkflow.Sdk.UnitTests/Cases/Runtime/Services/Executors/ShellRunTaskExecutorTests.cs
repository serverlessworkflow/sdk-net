namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class ShellRunTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Shell_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Shell = new ShellProcessDefinition { Command = "echo hello" } }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);

        var executor = new ShellRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ShellRunTaskExecutor>>(),
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

}
