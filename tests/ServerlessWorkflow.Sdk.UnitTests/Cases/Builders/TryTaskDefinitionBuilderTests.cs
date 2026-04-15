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

public class TryTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Try_With_Tasks_And_Catch()
    {
        //arrange
        var taskName = "risky";
        var key = "k";
        var value = "v";
        var expectedCount = 1;

        //act
        var task = new TryTaskDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Catch(c => { })
            .Build();

        //assert
        task.Try.Should().HaveCount(expectedCount);
        task.Try.Keys.Should().Contain(taskName);
        task.Catch.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Try_Tasks_Missing()
    {
        //arrange
        var builder = new TryTaskDefinitionBuilder();

        //act
        var act = () => builder
            .Catch(c => { })
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Catch_Missing()
    {
        //arrange
        var taskName = "step";
        var key = "k";
        var value = "v";

        //act
        var act = () => new TryTaskDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
