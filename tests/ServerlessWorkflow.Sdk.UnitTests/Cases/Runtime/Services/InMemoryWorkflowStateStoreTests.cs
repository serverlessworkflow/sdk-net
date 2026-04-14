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
        var store = new InMemoryWorkflowStore(cache);
        var definition = new WorkflowDefinition
        {
            Document = new WorkflowDefinitionMetadata { Dsl = "1.0.0", Name = "test-workflow", Namespace = "test", Version = "1.0.0" },
            Do = []
        };

        //act
        var result = await store.AddAsync(definition, null, TestContext.Current.CancellationToken);

        //assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetAsync_Should_Return_Stored_State()
    {
        //arrange
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var store = new InMemoryWorkflowStore(cache);
        var definition = new WorkflowDefinition
        {
            Document = new WorkflowDefinitionMetadata { Dsl = "1.0.0", Name = "test-workflow", Namespace = "test", Version = "1.0.0" },
            Do = []
        };
        var added = await store.AddAsync(definition, null, TestContext.Current.CancellationToken);

        //act
        var result = await store.GetAsync(added.Id, TestContext.Current.CancellationToken);

        //assert
        result.Should().Be(added);
    }

    [Fact]
    public async Task GetAsync_Should_Throw_When_Not_Found()
    {
        //arrange
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var store = new InMemoryWorkflowStore(cache);
        var id = "wf-missing";

        //act
        var act = () => store.GetAsync(id, TestContext.Current.CancellationToken);

        //assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

}
