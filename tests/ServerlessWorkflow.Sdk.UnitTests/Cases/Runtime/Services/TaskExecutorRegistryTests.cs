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
