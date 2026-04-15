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

using ServerlessWorkflow.Sdk.Runtime.Models;
using RuntimeJsonSerializationContext = ServerlessWorkflow.Sdk.Runtime.Serialization.Json.JsonSerializationContext;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class WorkflowInstanceTests
{

    static WorkflowInstance CreateInstance() => new()
    {
        Definition = new WorkflowDefinitionReference { Name = "test-workflow", Namespace = "test", Version = "1.0.0" }
    };

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = CreateInstance();
        //act
        var json = JsonSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.WorkflowInstance);
        var deserialized = JsonSerializer.Deserialize(json, RuntimeJsonSerializationContext.Default.WorkflowInstance);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(toSerialize.Id);
        deserialized.Definition.Name.Should().Be(toSerialize.Definition.Name);
        deserialized.Definition.Namespace.Should().Be(toSerialize.Definition.Namespace);
        deserialized.Definition.Version.Should().Be(toSerialize.Definition.Version);
        deserialized.Status.Should().Be(toSerialize.Status);
    }

    [Fact]
    public void Default_Values_Should_Be_Set()
    {
        //arrange & act
        var instance = CreateInstance();
        //assert
        instance.Id.Should().NotBeNullOrWhiteSpace();
        instance.Status.Should().Be(WorkflowStatus.Pending);
        instance.ContextData.Should().NotBeNull();
        instance.StartedAt.Should().BeNull();
        instance.EndedAt.Should().BeNull();
        instance.Output.Should().BeNull();
        instance.Error.Should().BeNull();
        instance.Runs.Should().BeNull();
    }

    [Fact]
    public async Task StartAsync_Should_Set_Status_To_Running()
    {
        //arrange
        var instance = CreateInstance();
        //act
        await instance.StartAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(WorkflowStatus.Running);
        instance.StartedAt.Should().NotBeNull();
        instance.Runs.Should().NotBeNull();
        instance.Runs!.Count.Should().Be(1);
    }

    [Fact]
    public async Task SuspendAsync_Should_Set_Status_To_Suspended()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        //act
        await instance.SuspendAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(WorkflowStatus.Suspended);
    }

    [Fact]
    public async Task ResumeAsync_Should_Set_Status_To_Running_And_Add_New_Run()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        await instance.SuspendAsync(TestContext.Current.CancellationToken);
        //act
        await instance.ResumeAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(WorkflowStatus.Running);
        instance.Runs.Should().NotBeNull();
        instance.Runs!.Count.Should().Be(2);
    }

    [Fact]
    public async Task SetOutputAsync_Should_Set_Status_To_Completed()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        var output = JsonNode.Parse("{\"result\": \"success\"}");
        //act
        await instance.SetOutputAsync(output, TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(WorkflowStatus.Completed);
        instance.Output.Should().NotBeNull();
        instance.EndedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task SetErrorAsync_Should_Set_Status_To_Faulted()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        var error = Error.Runtime(new Uri("https://example.com"), "something went wrong");
        //act
        await instance.SetErrorAsync(error, TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(WorkflowStatus.Faulted);
        instance.Error.Should().NotBeNull();
        instance.Error!.Title.Should().Be(ErrorTitle.Runtime);
        instance.EndedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task SetContextDataAsync_Should_Update_ContextData()
    {
        //arrange
        var instance = CreateInstance();
        var contextData = new JsonObject { ["key"] = "value" };
        //act
        await instance.SetContextDataAsync(contextData, TestContext.Current.CancellationToken);
        //assert
        instance.ContextData.Should().NotBeNull();
        instance.ContextData["key"]?.ToString().Should().Be("value");
    }

    [Fact]
    public async Task CancelAsync_Should_Set_Status_To_Cancelled()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        //act
        await instance.CancelAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(WorkflowStatus.Cancelled);
        instance.EndedAt.Should().NotBeNull();
    }

}
