using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class InMemoryTaskStateStoreTests
{

    [Fact]
    public async Task AddAsync_Should_Store_And_Return_State()
    {
        //arrange
        var store = new InMemoryTaskStateStore();
        var workflowId = "wf-1";
        var taskId = "task-1";
        var state = new Mock<ITaskInstance>();
        state.Setup(s => s.WorkflowId).Returns(workflowId);
        state.Setup(s => s.Id).Returns(taskId);

        //act
        var result = await store.AddAsync(state.Object, TestContext.Current.CancellationToken);

        //assert
        result.Should().Be(state.Object);
    }

    [Fact]
    public async Task GetAsync_Should_Return_Stored_State()
    {
        //arrange
        var store = new InMemoryTaskStateStore();
        var workflowId = "wf-1";
        var taskId = "task-1";
        var state = new Mock<ITaskInstance>();
        state.Setup(s => s.WorkflowId).Returns(workflowId);
        state.Setup(s => s.Id).Returns(taskId);
        await store.AddAsync(state.Object, TestContext.Current.CancellationToken);

        //act
        var result = await store.GetAsync(workflowId, taskId, TestContext.Current.CancellationToken);

        //assert
        result.Should().Be(state.Object);
    }

    [Fact]
    public async Task GetAsync_Should_Throw_When_Not_Found()
    {
        //arrange
        var store = new InMemoryTaskStateStore();
        var workflowId = "wf-missing";
        var taskId = "task-missing";

        //act
        var act = () => store.GetAsync(workflowId, taskId, TestContext.Current.CancellationToken);

        //assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task UpdateAsync_Should_Overwrite_Existing_State()
    {
        //arrange
        var store = new InMemoryTaskStateStore();
        var workflowId = "wf-1";
        var taskId = "task-1";
        var original = new Mock<ITaskInstance>();
        original.Setup(s => s.WorkflowId).Returns(workflowId);
        original.Setup(s => s.Id).Returns(taskId);
        original.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        await store.AddAsync(original.Object, TestContext.Current.CancellationToken);
        var updated = new Mock<ITaskInstance>();
        updated.Setup(s => s.WorkflowId).Returns(workflowId);
        updated.Setup(s => s.Id).Returns(taskId);
        updated.Setup<string>(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        //act
        await store.UpdateAsync(updated.Object, TestContext.Current.CancellationToken);
        var result = await store.GetAsync(workflowId, taskId, TestContext.Current.CancellationToken);

        //assert
        result.Status.Should().Be(Sdk.Runtime.TaskStatus.Completed);
    }

}
