// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
