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

public class WaitTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Duration()
    {
        //arrange
        var duration = Duration.FromSeconds(5);

        //act
        var task = new WaitTaskDefinitionBuilder()
            .For(duration)
            .Build();

        //assert
        task.Wait.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Accept_Duration_Via_Constructor()
    {
        //arrange
        var duration = Duration.FromMilliseconds(500);

        //act
        var task = new WaitTaskDefinitionBuilder(duration).Build();

        //assert
        task.Wait.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Throw_When_Duration_Missing()
    {
        //arrange
        var builder = new WaitTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
