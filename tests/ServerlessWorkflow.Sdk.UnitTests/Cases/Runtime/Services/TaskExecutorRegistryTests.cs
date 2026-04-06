namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class TaskExecutorRegistryTests
{

    [Fact]
    public void Resolve_Should_Return_Registered_Executor_Type()
    {
        //arrange
        var registry = new TaskExecutorRegistry();
        var taskType = "custom";
        registry.Register<SetTaskExecutor>(taskType);

        //act
        var resolved = registry.Resolve(taskType);

        //assert
        resolved.Should().Be(typeof(SetTaskExecutor));
    }

    [Fact]
    public void Resolve_Should_Return_Null_For_Unknown_TaskType()
    {
        //arrange
        var registry = new TaskExecutorRegistry();
        var unknownType = "nonexistent";

        //act
        var resolved = registry.Resolve(unknownType);

        //assert
        resolved.Should().BeNull();
    }

    [Fact]
    public void Register_By_Definition_Should_Map_Correctly()
    {
        //arrange
        var registry = new TaskExecutorRegistry();

        //act
        registry.Register<SetTaskDefinition, SetTaskExecutor>();
        var resolved = registry.Resolve(TaskType.Set);

        //assert
        resolved.Should().Be(typeof(SetTaskExecutor));
    }

    [Fact]
    public void Register_Should_Overwrite_Previous_Registration()
    {
        //arrange
        var registry = new TaskExecutorRegistry();
        var taskType = "test";
        registry.Register<SetTaskExecutor>(taskType);

        //act
        registry.Register<WaitTaskExecutor>(taskType);
        var resolved = registry.Resolve(taskType);

        //assert
        resolved.Should().Be(typeof(WaitTaskExecutor));
    }

}
