using Microsoft.Extensions.Caching.Memory;
using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class InMemoryWorkflowStateStoreTests
{

    [Fact]
    public async Task AddAsync_Should_Store_And_Return_State()
    {
        //arrange
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var store = new InMemoryWorkflowStateStore(cache);
        var id = "wf-1";
        var state = new Mock<IWorkflowState>();
        state.Setup(s => s.Id).Returns(id);

        //act
        var result = await store.AddAsync(state.Object, TestContext.Current.CancellationToken);

        //assert
        result.Should().Be(state.Object);
    }

    [Fact]
    public async Task GetAsync_Should_Return_Stored_State()
    {
        //arrange
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var store = new InMemoryWorkflowStateStore(cache);
        var id = "wf-1";
        var state = new Mock<IWorkflowState>();
        state.Setup(s => s.Id).Returns(id);
        await store.AddAsync(state.Object, TestContext.Current.CancellationToken);

        //act
        var result = await store.GetAsync(id, TestContext.Current.CancellationToken);

        //assert
        result.Should().Be(state.Object);
    }

    [Fact]
    public async Task GetAsync_Should_Throw_When_Not_Found()
    {
        //arrange
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var store = new InMemoryWorkflowStateStore(cache);
        var id = "wf-missing";

        //act
        var act = () => store.GetAsync(id, TestContext.Current.CancellationToken);

        //assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

}
