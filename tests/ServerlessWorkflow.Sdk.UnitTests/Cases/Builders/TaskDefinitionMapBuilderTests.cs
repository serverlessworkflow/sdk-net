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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TaskDefinitionMapBuilderTests
{

    [Fact]
    public void Build_Should_Create_Map_With_Tasks()
    {
        //arrange
        var step1Name = "step1";
        var step1Key = "a";
        var step1Value = "1";
        var step2Name = "step2";
        var step2Key = "b";
        var step2Value = "2";
        var expectedCount = 2;

        //act
        var map = new TaskDefinitionMapBuilder()
            .Do(step1Name, task => task.Set(step1Key, step1Value))
            .Do(step2Name, task => task.Set(step2Key, step2Value))
            .Build();

        //assert
        map.Should().HaveCount(expectedCount);
        map.Keys.Should().Contain(step1Name);
        map.Keys.Should().Contain(step2Name);
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Task()
    {
        //arrange
        var taskName = "step";
        var key = "k";
        var value = "v";
        var task = new SetTaskDefinition { Set = new JsonObject { [key] = value } };
        var expectedCount = 1;

        //act
        var map = new TaskDefinitionMapBuilder()
            .Do(taskName, task)
            .Build();

        //assert
        map.Should().HaveCount(expectedCount);
    }

    [Fact]
    public void Build_Should_Throw_When_Empty()
    {
        //arrange
        var builder = new TaskDefinitionMapBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
